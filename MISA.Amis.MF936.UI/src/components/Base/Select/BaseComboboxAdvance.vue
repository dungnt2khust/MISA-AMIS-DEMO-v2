<template lang="">
	<div
		class="comboboxadvance-wrapper"
		:class="'comboboxadvance--' + type"
		:style="{ 'max-width': width }"
	>
		<span v-if="label != ''" class="label">
			{{ label }} <span v-if="required" class="text-red">*</span>
		</span>
		<div
			class="comboboxadvance"
			:class="{ 'comboboxadvance--focus': focusState }"
		>
			<div class="comboboxadvance__main">
				<div class="comboboxadvance__input-wrapper">
					<input
						ref="comboboxInput"
						v-on="inputListeners"
						type="text"
						class="comboboxadvance__input"
						:tabindex="tabindex"
						:value="foundSelectedItem()"
						:placeholder="placeholder"
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
				:method="focusInput"
				:showList="showList"
				:focusState="focusState"
			/>
			<base-list-grid
				v-show="showList"
				:showList="showList"
				v-model="showList"
				:listGridStyle="listGridStyle"
				:listGridData="listGridData"
				:valueBind="valueBind"
				:vmodelField="vmodelField"
				:subfield="subfield"
				@changeOption="changeOption($event)"
				:type="type"
			>
				<template v-slot:footer>
					<div @click="showForm()" class="comboboxadvance__add">
						<div class="comboboxadvance__icon"></div>
						Thêm mới (F9)
					</div>
				</template>
			</base-list-grid>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import axios from "axios";
	// COMPONENTS
	import BaseDropdownButton from "../Select/BaseDropdownButton.vue";
	import BaseListGrid from "../Select/BaseListGrid.vue";

	export default {
		name: "BaseComboboxAdvance",
		components: {
			BaseDropdownButton,
			BaseListGrid,
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
			tabindex: {
				type: Number,
				default: -1,
			},
			api: {
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
			valueBind: {
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
				default: -1
			}
		},
		data() {
			return {
				showList: false,
				focusState: false,
				dropdownButtonState: this.type != "small",
				listGridData: [],
				inputTimeout: null,
			};
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
					blur: () => {
						this.focusState = false;
						if (this.showList) {
							this.showList = false;
							this.dropdownButtonState = false;
						}
					},
					input: (event) => {
						clearTimeout(this.inputTimeout);

						this.inputTimeout = setTimeout(() => {
							if (!this.data)
								axios
									.get(this.api + `&searchData=${event.target.value}`)
									.then((res) => {
										console.log(res);
										this.listGridData = res.data[this.controller];
									})
									.catch((res) => {
										console.log(res);
									});
						}, 500);
					},
				});
			},	
		},
		methods: {
			/**
			 * Giá trị hiện tại
			 * @param {String} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			foundSelectedItem() {
				if (this.valueBind) {
					var foundIdx = this.listGridData.findIndex((item) => {
						if (this.subfield)
							return item[this.subfield] == this.valueBind;
						else
							return item[this.vmodelField] == this.valueBind;
					});
					if (foundIdx != -1) {
						if (this.display)
							return this.listGridData[foundIdx][this.display];	
						else 
							return this.listGridData[foundIdx][this.field];
					} else {
						return this.default;
					}
				} else return "";
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
			changeOption(value) {
				if (value != this.valueBind) {
					this.$emit("input", value);
					this.$nextTick(() => {
						switch(this.controller) {
							case "Commoditys":
								this.$bus.$emit('changeCommodity', this.index, this.valueBind, this.listGridData);
								break;
							// case "Units}"
						}

					})	
				}
			},
			/**
			 * Mở form
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			showForm() {
				this.$bus.$emit(this.form);
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
					if (!this.data)
						axios
							.get(this.api)
							.then((res) => {
								console.log(res);
								this.listGridData = res.data[this.controller];
							})
							.catch((res) => {
								console.log(res);
							});
					else this.listGridData = this.data;
				}
			},
			/**
			 * Giá trị bind thay đổi thì cập nhật lại dữ liệu
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			valueBind: function() {
				this.foundSelectedItem();
			}
		},
	};
</script>
<style>
	@import url("../../../css/base/select/comboboxadvance.css");
</style>
