<template lang="">
	<div
		class="comboboxadvance-wrapper"
		:style="{ width: width }"
		:class="'comboboxadvance--' + type"
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
						:placeholder="placeholder"
					/>
				</div>
				<div class="comboboxadvance__icon"></div>
			</div>
			<base-dropdown-button v-model="showList" />
			<base-list-grid v-show="showList" :showList="showList" />
		</div>
	</div>
</template>
<script>
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
				default: ''
			},
			tabindex: {
				type: Number,
				default: -1
			}
		},
		data() {
			return {
				showList: false,
				focusState: false,
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
						event.target.select();
					},
					blur: () => {
						this.focusState = false;
						setTimeout(() => {
							if (this.showList) {
								this.showList = false;
							}
						}, 100);
					},
				});
			},
		},
		watch: {
			/**
			 * Bắt sự kiện thay đổi trạng thái
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG (23/09/2021)
			 */
			showList: function(value) {
				if (value) {
					this.$refs.comboboxInput.focus();
				}
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/comboboxadvance.css");
</style>
