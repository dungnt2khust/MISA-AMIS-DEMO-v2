<template lang="">
	<div
		class="comboboxadvance-wrapper"
		:class="['comboboxadvance--' + type]"
		:style="{ 'max-width': width }"
	>
		<span v-if="label != ''" class="label">
			{{ label }}
			<span v-if="required && showRequired" class="text-red">*</span>
		</span>
		<div v-if="disable" class="input__span">{{ inputValue }}</div>
		<div
			v-if="!disable"
			class="comboboxadvance"
			:class="{
				'comboboxadvance--focus': focusState,
				'border-error': errorMsg ? true : false,
			}"
			ref="comboboxadvance"
		>
			<div
				class="comboboxadvance__main"
				:class="{ 'comboboxadvance--disable': !enable }"
			>
				<div class="comboboxadvance__input-wrapper">
					<input
						ref="comboboxInput"
						v-on="inputListeners"
						type="text"
						class="comboboxadvance__input"
						v-model="inputValue"
						:placeholder="placeholder"
						:readonly="!enable"
						:title="errorMsg != '' ? errorMsg : ''"
					/>
				</div>
				<div
					v-if="type != 'small'"
					@click="showForm()"
					class="comboboxadvance__icon"
				></div>
			</div>
			<base-dropdown-button
				v-show="dropdownButtonState"
				v-model="showList"
				:showList="showList"
				:focusState="focusState"
				:enable="enable"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
	import methods from "../../../mixins/methods";
	import baseAPI from "../../../js/base/baseAPI.js";
	// COMPONENTS
	import BaseDropdownButton from "../Select/BaseDropdownButton.vue";

	export default {
		name: "BaseComboboxAdvance",
		mixins: [globalComponents, methods],
		components: {
			BaseDropdownButton,
		},
		props: {
			label: {
				type: String,
				default: "",
			},
			required: {
				type: Boolean,
				default: false,
			},
			width: {
				type: String,
				default: "",
			},
			type: {
				type: String,
				default: "",
			},
			placeholder: {
				type: String,
				default: "",
			},
			controller: {
				type: String,
				default: "",
			},
			listGridStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			value: {
				type: String,
				default: "",
			},
			vmodelField: {
				type: String,
				default: "",
			},
			field: {
				type: String,
				default: "",
			},
			default: {
				type: String,
				default: "",
			},
			display: {
				type: String,
				default: "",
			},
			data: {
				type: Array,
				default: null,
			},
			subfield: {
				type: String,
				default: "",
			},
			form: {
				type: String,
				default: "",
			},
			index: {
				type: Number,
				default: -1,
			},
			enable: {
				type: Boolean,
				default: true,
			},
			disable: {
				type: Boolean,
				default: false,
			},
			hasFooter: {
				type: Boolean,
				default: false,
			},
			syncfield: {
				type: String,
				default: "",
			},
			formName: {
				type: String,
				default: "",
			},
			name: {
				type: String,
				default: "",
			},
			escapeValue: {
				type: [Array, String],
				default: null,
			},
			showRequired: {
				type: Boolean,
				default: false
			}
		},
		data() {
			return {
				showList: false,
				focusState: false,
				dropdownButtonState: this.type != "small",
				listGridData: [],
				inputTimeout: null,
				errorMsg: "",
				inputValue: "",
				filterString: "",
				baseAPI: new baseAPI(this.controller, this.vmodelField),
				size: null,
			};
		},
		created() {
			this.$bus.$on("validate" + this.formName, () => {
				this.validateCombobox();
				if (this.errorMsg)
					this.$bus.$emit(
						"catchError" + this.formName,
						this.errorMsg,
						this.$refs.comboboxInput
					);
			});
			// nhận lại dữ liệu
			this.$bus.$on("changeOption" + this.vmodelField, (newValue, index) => {
				if (index == this.index) {
					this.changeOption(newValue);
					this.showList = false;
				}
			});
		},
		computed: {
			/**
			 * Bắt các sự kiện của input
			 * CreatedBy: NTDUNG (23/09/2021)
			 */
			inputListeners() {
				return Object.assign({}, this.$listener, {
					focus: (event) => {
						this.focusState = true;
						this.dropdownButtonState = true;
						event.target.select();
					},
					input: () => {
						clearTimeout(this.inputTimeout);

						this.inputTimeout = setTimeout(() => {
							if (!this.data) {
								this.filterString = this.inputValue;
								this.loadData();
							}
						}, 500);
					},
					// blur: () => {
					// 	this.$nextTick(() => {
					// 		this.focusState = false;
					// 		this.showList = false;
					// 	});
					// },
				});
			},	
		},
		methods: {
			/**
			 * Giá trị hiện tại
			 * @param {String} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 * ModifiedBy: NTDUNG (05/10/2021)
			 */
			setInputValue() {
				if (this.value) {
					var foundIdx = this.listGridData.findIndex((item) => {
						if (this.subfield) return item[this.subfield] == this.value;
						else return item[this.vmodelField] == this.value;
					});
					if (foundIdx != -1) {
						if (this.display)
							this.inputValue = this.listGridData[foundIdx][this.display];
						else this.inputValue = this.listGridData[foundIdx][this.field];
					} else {
						this.inputValue = this.default;
					}
				} else this.inputValue = "";
			},
			/**
			 * Focus input khi nút dropdown được nhấn
			 * CreatedBy: NTDUNG (26/09/2021)
			 */
			focusInput() {
				this.$nextTick(() => {
					if (!this.focusState) {
						this.focusState = true;
						this.dropdownButtonState = true;
					}
				});
			},
			/**
			 * Khi thay đổi giá trị thì cập nhật
			 * @param {String} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			changeOption(newValue) {
				if (newValue != this.value) {
					this.$emit("input", newValue);
					this.$nextTick(() => {
						if (this.syncfield) {
							this.$bus.$emit(
								"change" + this.syncfield,
								this.index,
								this.value,
								this.listGridData
							);
						}
					});
				}
			},
			/**
			 * Mở form
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			showForm() {
				this.showList = false;
				if (!this.form) {
					this.callDialog(
						this.$enum.DIALOG_TYPE.Warn,
						this.$resourcesVN.NOTIFY.FeatureNotAvaiable
					);
				} else if (this.enable) this.$bus.$emit(this.form);
			},
			/**
			 * Validate combobox
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			validateCombobox() {
				// Reset lỗi
				this.errorMsg = "";
				// Check required
				if (this.required) {
					if (!this.inputValue) {
						if (this.label)
							this.errorMsg = this.formatString(
								this.$resourcesVN.NOTIFY.FieldRequired,
								this.label
							);
						else
							this.errorMsg = this.formatString(
								this.$resourcesVN.NOTIFY.FieldRequired,
								this.name
							);
					} else {
						this.errorMsg = "";
					}
				}
			},
			/**
			 * Tải dữ liệu
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			loadData() {
				if (!this.data)
					this.baseAPI
						.getFilterPaging(this.filterString, 1, 20)
						.then((res) => {
							console.log(res);
							this.listGridData = res.data[this.controller];
							this.showListGrid();
							// if (this.escapeValue) {
							// 	if (!Array.isArray(this.escapeValue)) {
							// 		var foundIdx = this.listGridData.findIndex((item) => {
							// 			return item[this.vmodelField] == this.escapeValue;
							// 		});
							// 		this.listGridData.splice(foundIdx, 1);
							// 	} else {
							// 		this.escapeValue.forEach((item) => {
							// 			var foundIdx = this.listGridData.findIndex((subItem) => {
							// 				return subItem[this.vmodelField] == item;
							// 			});
							// 			this.listGridData.splice(foundIdx, 1);
							// 		});
							// 	}
							// }
						})
						.catch((res) => {
							console.log(res);
						});
				else {
					this.listGridData = this.data;
					this.showListGrid();
				}
			},
			/**
			 * Hiện list
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			showListGrid() {
				var size = this.$refs.comboboxadvance.getBoundingClientRect();
				this.size = {
					top: size.top,
					left: size.left,
					right: size.right,
					bottom: size.bottom,
					height: size.height,
					width: size.width,
				};
				this.$bus.$emit("showListGrid", {
					type: this.type,
					listSelected: this.listSelected,
					listGridStyle: this.listGridStyle,
					listGridData: this.listGridData,
					valueBind: this.value,
					vmodelField: this.vmodelField,
					subfield: this.subfield,
					controller: this.controller,
					hasFooter: this.hasFooter,
					size: this.size,
					name: this.vmodelField,
					form: this.form,
					index: this.index,
				});
			},
		},
		watch: {
			/**
			 * KHi mở dropdown thì gọi api
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			showList: function(value) {
				if (value) {
					this.filterString = "";
					this.loadData();
				} else {
					this.$bus.$emit("hideListGrid");
					if (!this.focusState && this.type == "small")
						this.dropdownButtonState = false;
				}
			},
			/**
			 * Giá trị bind thay đổi thì cập nhật lại dữ liệu
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			value: {
				handler() {
					this.errorMsg = "";
					this.setInputValue();
				},
				deep: true,
				immediate: true,
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/comboboxadvance.css");
</style>
