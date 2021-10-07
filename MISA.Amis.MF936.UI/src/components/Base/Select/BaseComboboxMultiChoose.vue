<template lang="">
	<div class="comboboxmultichoose">
		<div class="comboboxadvance-wrapper" :class="['comboboxadvance--' + type]">
			<span v-if="label != ''" class="label">
				{{ label }} <span v-if="required" class="text-red">*</span>
			</span>
			<div
				class="comboboxadvance"
				:class="{ 'comboboxadvance--focus': focusState }"
				:style="{ width: width }"
			>
				<div class="comboboxadvance__main">
					<div class="comboboxadvance__input-wrapper" style="padding: 0">
						<div class="comboboxadvance__selection">
							<div
								v-for="(item, index) in listSelected"
								class="comboboxadvance__selection-item"
								:key="index"
							>
								<div class="comboboxadvance__selection-title">
									{{ item[listGridStyle[0]["field"]] }}
								</div>
								<div
									@click="listSelected.splice(index, 1)"
									class="comboboxadvance__selection-icon"
								></div>
							</div>
							<input
								v-on="inputListeners"
								v-model="filterString"
								type="text"
								class="comboboxadvance__input-multichoose"
							/>
						</div>
					</div>
					<div
						v-if="type != 'small'"
						@click="showForm()"
						class="comboboxadvance__icon"
					></div>
				</div>
				<base-dropdown-button v-model="showList" :showList="showList" />
				<base-list-grid-multichoose
					v-show="showList"
					:showList="showList"
					v-model="listSelected"
					@hideGrid="showList = false"
					:listSelected="listSelected"
					:listGridStyle="listGridStyle"
					:listGridData="listGridData"
					:vmodelField="vmodelField"
				/>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import baseAPI from "../../../js/base/baseAPI.js";
	// COMPONENTS
	import BaseDropdownButton from "./BaseDropdownButton.vue";
	import BaseListGridMultichoose from "./BaseListGridMultichoose.vue";

	export default {
		name: "BaseComboboxMultichoose",
		components: {
			BaseDropdownButton,
			BaseListGridMultichoose,
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
				type: Array,
				default: function() {
					return [];
				},
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
				type: Array,
				default: function() {
					return [];
				},
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
			syncdata: {
				type: Boolean,
				default: false,
			},
			defaultIndex: {
				type: Number,
				default: -1,
			},
		},
		data() {
			return {
				showList: false,
				listSelected: [],
				listGridData: [],
				baseAPI: new baseAPI(this.controller),
				filterString: "",
				focusState: false,
				inputTimeout: null,
			};
		},
		computed: {
			inputListeners() {
				return Object.assign({}, this.$listener, {
					input: () => {
						clearTimeout(this.inputTimeout);

						this.inputTimeout = setTimeout(() => {
							this.loadData(true);
						}, 500)
					},
					blur: () => {
						this.focusState = false;
					},
					focus: () => {
						this.focusState = true;
					},
				});
			},
		},
		methods: {
			/**
			 * Mở form
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			showForm() {
				if (!this.form) {
					this.callDialog(
						this.$enum.DIALOG_TYPE.Warn,
						this.$resourcesVN.NOTIFY.FeatureNotAvaiable
					);
				} else if (this.enable) this.$bus.$emit(this.form);
			},
			/**
			 * Load data
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			loadData(value, mode) {
				if (value) {
					if (!this.data)
						this.baseAPI
							.getFilterPaging(this.filterString, 1, 20)
							.then((res) => {
								console.log(res);
								this.listGridData = res.data[this.controller];
								if (mode) {
									this.listSelected = [this.listGridData[this.defaultIndex]];
								}
							})
							.catch((res) => {
								console.log(res);
							});
					else this.listGridData = this.data;
				}
			},
			/**
			 * Thay đổi trạng thái focus
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG(05/10/2021)
			 */
			changeFocusState(value) {
				this.focusState = value;
			},
		},
		watch: {
			/**
			 * KHi mở dropdown thì gọi api
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			showList: function(value) {
				this.loadData(value);
			},
			listSelected: {
				handler(value) {
					var newData = [];
					value.forEach((item) => {
						newData.push(item[this.vmodelField]);
					});
					this.$emit("input", newData);
				},
				deep: true,
			},
			defaultIndex: {
				handler(value) {
					if (value != -1) {
						this.loadData(true, 1);
					}
				},
				immediate: true,
			},
		},
	};
</script>
<style></style>
