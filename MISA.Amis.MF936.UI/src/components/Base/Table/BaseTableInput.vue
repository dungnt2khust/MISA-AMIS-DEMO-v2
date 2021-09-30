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
						:class="positionOfRecord(item)"
						:key="index"
					>
						{{ item["name"] }}
					</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<tr
					v-for="(itemData, indexData) in tableData"
					v-show="itemData['state'] != 2"
					:key="indexData"
				>
					<td class="fx-center">{{ indexData }}</td>
					<td
						v-for="(itemStyle, indexStyle) in tableStyle"
						:class="positionOfRecord(itemStyle)"
						:key="indexStyle"
					>
						<base-combobox-advance
							v-if="
								itemStyle.type == TableDataStyle.TYPE.Combobox ||
									itemStyle.type == TableDataStyle.TYPE.ComboboxNotAdd
							"
							:api="itemStyle['api']"
							:controller="itemStyle['controller']"
							v-model="itemData[itemStyle['vmodel']]"
							:valueBind="itemData[itemStyle['vmodel']]"
							:vmodelField="itemStyle['vmodel']"
							:field="itemStyle['field']"
							:default="
								itemStyle['default']
									? itemData[itemStyle['default']]
									: itemData[itemStyle['field']]
							"
							:display="itemStyle['display']"
							:listGridStyle="itemStyle['style']"
							:data="itemData[itemStyle['data']]"
							:subfield="itemStyle['subfield']"
							:form="itemStyle['form']"
							type="small"
						/>
						<base-input
							v-if="itemStyle.type == TableDataStyle.TYPE.Input"
							:value="itemData[itemStyle['field']]"
							v-model="itemData[itemStyle['field']]"
							:pos="itemStyle.pos"
						/>
						<base-input-date
							v-if="itemStyle.type == TableDataStyle.TYPE.InputDate"
							:value="itemData[itemStyle['field']]"
							v-model="itemData[itemStyle['field']]"
						/>
					</td>
					<td>
						<div class="tableinput__delete"></div>
					</td>
				</tr>
			</tbody>
		</table>
		<div class="tableinput__function mt-20 mb-20">
			<base-button :method="addRecord" label="Thêm dòng" type="small" />
			<base-button class="ml-10" label="Thêm ghi chú" type="small" />
			<base-button
				:method="deleteAllRecords"
				class="ml-10"
				label="Xoá hết dòng"
				type="small"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import TableDataStyle from "../../../js/enum/tableDataStyle";
	import VoucherDetailState from "../../../js/enum/voucherDetailState";
	// COMPONENTS
	import BaseInput from "../BaseInput.vue";
	import BaseInputDate from "../BaseInputDate.vue";
	import BaseComboboxAdvance from "../Select/BaseComboboxAdvance.vue";
	import BaseButton from "../Button/BaseButton.vue";

	export default {
		name: "BaseTableInput",
		components: {
			BaseInput,
			BaseComboboxAdvance,
			BaseButton,
			BaseInputDate,
		},
		props: {
			tableStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			tableData: {
				type: Array,
				default: function() {
					return [];
				},
			},
		},
		data() {
			return {
				TableDataStyle: TableDataStyle,
			};
		},
		methods: {
			/**
			 * Đặt vị trí cho bản ghi
			 * @param {Object} itemStyle
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			positionOfRecord(itemStyle) {
				var styleClass = {};
				switch (itemStyle.pos) {
					case TableDataStyle.POS.Left:
						styleClass["text-align-left"] = true;
						break;
					case TableDataStyle.POS.Center:
						styleClass["text-align-center"] = true;
						break;
					case TableDataStyle.POS.Right:
						styleClass["text-align-right"] = true;
						break;
				}
				return styleClass;
			},
			/**
			 * Thêm một bản ghi rỗng vào trong bảng
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			addRecord() {
				var rowEmpty = {};
				this.tableStyle.forEach((item) => {
					rowEmpty[item["field"]] = null;
				});
				rowEmpty["state"] = VoucherDetailState.ADD;
				var tableData = this.tableData;
				tableData.push(rowEmpty);
				this.changeTableData(tableData);
			},
			/**
			 * Xoá tất cả các bản ghi
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			deleteAllRecords() {
				var tableData = this.tableData;
				tableData.forEach((item) => {
					item["state"] = VoucherDetailState.DELETE;
				});
				this.changeTableData(tableData);
			},
			/**
			 * Thay đổi dữ liệu trong bảng
			 * @param {Array} newValue
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			changeTableData(newValue) {
				this.$emit("input", newValue);
			},
		},
	};
</script>
<style scoped>
	@import url("../../../css/base/table/tableinput.css");
</style>
