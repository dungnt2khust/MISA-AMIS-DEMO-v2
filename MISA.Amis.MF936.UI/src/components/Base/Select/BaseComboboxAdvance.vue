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
						:placeholder="placeholder"
					/>
				</div>
				<div v-if="type != 'small'" class="comboboxadvance__icon"></div>
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
				:listGridStyle="listGridStyle"
				:listGridData="listGridData"
				:type="type"
			>
				<template v-slot:footer>
					<div class="comboboxadvance__add">
						<div class="comboboxadvance__icon"></div>
						Thêm mới (F9)
					</div>
				</template>
			</base-list-grid>
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
				default: "",
			},
			tabindex: {
				type: Number,
				default: -1,
			},
		},
		data() {
			return {
				showList: false,
				focusState: false,
				dropdownButtonState: this.type != "small",
				listGridStyle: [
					{ name: "Mã đối tượng", field: "object_code", width: "150px" },
					{ name: "Tên đối tượng", field: "object_name", width: "200px" },
					{ name: "Mã số thuế", field: "tax_code", width: "150px" },
					{ name: "Địa chỉ", field: "address", width: "200px" },
					{ name: "Số điện thoại", field: "phone_number", width: "100px" },
				],
				listGridData: [
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
					{
						object_code: "KH001",
						object_name: "Tên đối tượng",
						tax_code: "Mã số thuế",
						address: "Địa chỉ",
						phone_number: "Số điện thoại",
					},
				],
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
				});
			},
		},
		methods: {
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
		},
	};
</script>
<style>
	@import url("../../../css/base/select/comboboxadvance.css");
</style>
