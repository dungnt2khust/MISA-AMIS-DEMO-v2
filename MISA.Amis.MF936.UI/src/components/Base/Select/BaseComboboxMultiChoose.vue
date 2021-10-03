<template lang="">
	<div class="comboboxmultichoose">
		<div class="comboboxadvance-wrapper" :class="'comboboxadvance--' + type">
			<span v-if="label != ''" class="label">
				{{ label }} <span v-if="required" class="text-red">*</span>
			</span>
			<div class="comboboxadvance" :style="{ width: width }">
				<div class="comboboxadvance__main">
					<div class="comboboxadvance__input-wrapper" style="padding: 0">
						<div class="comboboxadvance__selection">
							<div
								v-for="(item, index) in listSelected"
								class="comboboxadvance__selection-item"
								:key="index"
							>
								<div class="comboboxadvance__selection-title">
									{{ listGridData[item][listGridStyle[0]["field"]] }}
								</div>
								<div
									@click="listSelected.splice(index, 1)"
									class="comboboxadvance__selection-icon"
								></div>
							</div>
						</div>
					</div>
					<div v-if="type != 'small'" @click="showForm()" class="comboboxadvance__icon"></div>
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
				/>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import axios from "axios";
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
				type: Array,
				default: function() {
					return [];
				}
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
		},
		data() {
			return {
				showList: false,
				listSelected: [],
				listGridData: []
			};
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
			listSelected: {
				handler(value) {
					var newData = [];
					value.forEach(item => {
						newData.push(this.listGridData[item][this.vmodelField]);
					});
					this.$emit('input', newData);
				},
				deep: true,
			},
		},
	};
</script>
<style></style>
