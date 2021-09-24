<template lang="">
	<table class="table">
		<thead>
			<tr>
				<th>
					<base-checkbox />
				</th>
				<th
					v-for="(item, index) in tableStyle"
					:style="{ 'min-width': item['width'] }"
					:key="index"
				>
					{{ item["name"] }}
				</th>
				<th>{{ $resourcesVN.FUNCTION.TableFunction }}</th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="(itemData, indexData) in tableData" :key="indexData">
				<td>
					<base-checkbox />
				</td>
				<td
					v-for="(itemStyle, indexStyle) in tableStyle"
					:class="recordStyle(itemStyle)"
					:key="indexStyle"
				>
					{{
						formatTableData(itemData[itemStyle["field"]], itemStyle["field"])
					}}
				</td>
				<td>
					<div
						class="table__function"
						:class="{ 'table--loading': tableLoading }"
					>
						<div class="table__update">
							{{ $resourcesVN.TABLE.Watch }}
						</div>
						<div
							v-on="
								contextMenuListeners([
									{
										name: $resourcesVN.FUNCTION.UnCommit,
										function: () => {
											functionTest($resourcesVN.FUNCTION.UnCommit);
										},
									},
									{
										name: $resourcesVN.FUNCTION.Replication,
										function: () => {
											functionTest($resourcesVN.FUNCTION.Replication);
										},
									},
								])
							"
							class="context-menu"
							tabindex="0"
						>
							<div class="context-menu__icon"></div>
						</div>
					</div>
				</td>
			</tr>
		</tbody>
	</table>
</template>
<script>
	// LIBRARY
	import methods from "../../../mixins/methods";
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
	import TableDataStyle from "../../../js/enum/tableDataStyle.js"

	// COMPONENTS
	import BaseCheckbox from "../BaseCheckbox.vue";

	export default {
		name: "BaseTable",
		mixins: [globalComponents, methods],
		components: {
			BaseCheckbox,
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
			tableLoading: {
				type: Boolean,
				default: false,
			},
		},
		data() {
			return {};
		},
		computed: {},
		methods: {
			/**
			 * Đặt kiểu cho ô dữ liệu
			 * @param {Object} itemStyle kiểu được lưu trong object
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			recordStyle(itemStyle) {
				var styleClass = {};
				// Kiểu text đặc biệT
				switch (itemStyle["color"]) {
					case TableDataStyle.COLOR.Blue:
						styleClass["text-blue"] = true;
						break;
				}
				// Vị trí cho dữ liệu trong ô
				switch (itemStyle["pos"]) {
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
			 * Định dạng dữ liệu trước khi bind lên giao diện
			 * @param {Number, String} value
			 * @param {String} field
			 * @return {Number, String}
			 */
			formatTableData(value, field) {
				var formatedValue;

				if (field.includes("date")) {
					formatedValue = this.formatDate(value);
				} else {
					formatedValue = value;
				}
				return formatedValue;
			},
			functionTest(index) {
				console.log("option" + index);
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/table/table.css");
</style>
