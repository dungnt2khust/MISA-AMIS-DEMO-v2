<template lang="">
	<div class="tableinput-wrapper">
		<div class="tableinput__title">Hàng tiền</div>
		<table class="tableinput">
			<thead>
				<tr>
					<th class="fx-center">#</th>
					<th
						v-for="(item, index) in tableStyle"
						:style="{ 'min-width': item['width'] }"
						:key="index"
					>
						{{ item["name"] }}
					</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<tr v-for="(itemData, indexData) in tableData" :key="indexData">
					<td class="fx-center">{{ indexData }}</td>
					<td v-for="(itemStyle, indexStyle) in tableStyle" :key="indexStyle">
						<base-input v-if="indexStyle % 2 == 0"/>
						<base-combobox-advance type="small" v-if="indexStyle % 2 != 0"/>
					</td>
					<td>
						<div class="tableinput__delete"></div>
					</td>
				</tr>
			</tbody>
		</table>
        <div class="tableinput__function mt-20 mb-20">
            <base-button :method="addRecord" label="Thêm dòng" type="small"/>
            <base-button :method="openSelectCommodity" class="ml-10" label="Thêm ghi chú" type="small"/>
            <base-button :method="deleteAllRecords" class="ml-10" label="Xoá hết dòng" type="small"/>
        </div>
	</div>
</template>
<script>
    // LIBRARY
    import TableDataStyle from '../../../js/enum/tableDataStyle'
    // COMPONENTS 
    import BaseInput from '../BaseInput.vue'
    import BaseComboboxAdvance from '../Select/BaseComboboxAdvance.vue'
    import BaseButton from "../Button/BaseButton.vue"

	export default {
		name: "BaseTableInput",
        components: {
            BaseInput,
            BaseComboboxAdvance,
            BaseButton
        },
		data() {
			return {
				tableStyle: [
					{
						name: "Mã hàng",
						field: "commodity_code",
						width: "150px",
                        type: TableDataStyle.Combobox,
						enable: true,
					},
					{
						name: "Tên hàng",
						field: "commodity_name",
						width: "300px",
						enable: true,
					},
					{
						name: "Kho",
						field: "warehouse_code",
						width: "150px",
						enable: true,
					},
					{
						name: "TK nợ",
						field: "account_number1",
						width: "150px",
						enable: true,
					},
					{
						name: "TK có",
						field: "account_number2",
						width: "150px",
						enable: true,
					},
					{ name: "ĐVT", field: "unit", width: "150px", enable: true },
					{ name: "Số lượng", field: "amount", width: "150px", enable: true },
					{ name: "Đơn giá", field: "price", width: "150px", enable: true },
					{
						name: "Thành tiền",
						field: "summary",
						width: "100px",
						enable: true,
					},
				],
				tableData: [
					{
						commodity_code: "MF936",
						commodity_name: "Nguyễn Tiến Dũng",
						warehouse_code: "MFFFF",
						account_number1: "0372973290",
						account_number2: "0372973290",
						rules: "None",
						unit: "Kg",
						amount: 1,
						price: 10000000,
						summary: 10000000,
					},
					{
						commodity_code: "MF936",
						commodity_name: "Nguyễn Tiến Dũng",
						warehouse_code: "MFFFF",
						account_number1: "0372973290",
						account_number2: "0372973290",
						rules: "None",
						unit: "Kg",
						amount: 1,
						price: 10000000,
						summary: 10000000,
					},
				],
			};
		},
		methods: {
			addRecord() {
				this.tableData.push({
						commodity_code: "MF936",
						commodity_name: "Nguyễn Tiến Dũng",
						warehouse_code: "MFFFF",
						account_number1: "0372973290",
						account_number2: "0372973290",
						rules: "None",
						unit: "Kg",
						amount: 1,
						price: 10000000,
						summary: 10000000,
					});
			},
			deleteAllRecords() {
				this.tableData = [];
			},
			openSelectCommodity() {
				this.$emit('input', true);
			}
		}
	};
</script>
<style>
	@import url("../../../css/base/table/tableinput.css");
</style>
