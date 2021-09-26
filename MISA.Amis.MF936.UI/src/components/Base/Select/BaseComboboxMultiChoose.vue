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
					<div v-if="type != 'small'" class="comboboxadvance__icon"></div>
				</div>
				<base-dropdown-button v-model="showList" :showList="showList" />
				<base-list-grid-multichoose
					v-show="showList"
					:showList="showList"
					v-model="listSelected"
					:listSelected="listSelected"
					:listGridStyle="listGridStyle"
					:listGridData="listGridData"
				/>
			</div>
		</div>
	</div>
</template>
<script>
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
		},
		data() {
			return {
				showList: false,
				listSelected: [],
				listGridStyle: [
					{
						name: "Mã nhóm vật tư, hàng hoá, dịch vụ",
						field: "commodity_group_code",
						width: "220px",
					},
					{
						name: "Tên nhóm vật tư, hàng hoá, dịch vụ",
						field: "commodity_group_name",
						width: "200px",
					},
				],
				listGridData: [
					{
						commodity_group_code: "HH",
						commodity_group_name: "Hàng hoá",
					},
					{
						commodity_group_code: "DV",
						commodity_group_name: "Dịch vụ",
					},
					{
						commodity_group_code: "NVL",
						commodity_group_name: "Nguyên vật liệu",
					},
				],
			};
		},
		watch: {
			listSelected: {
				handler(value) {
					console.log(value);
				},
				deep: true,
			},
		},
	};
</script>
<style></style>
