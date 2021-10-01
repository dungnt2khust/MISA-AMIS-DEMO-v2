<template lang="">
	<table class="table">
		<thead>
			<tr>
				<th>
					<base-checkbox :state="checkAll" v-model="checkAll" />
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
			<tr
				v-for="(itemData, indexData) in tableData"
				:class="{ 'text--green': !itemData['is_mention'] }"
				:key="indexData"
			>
				<td>
					<base-checkbox
						:state="markRows[indexData]"
						v-model="markRows[indexData]"
					/>
				</td>
				<td
					v-for="(itemStyle, indexStyle) in tableStyle"
					@click="choose(indexStyle, itemData[tableId])"
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
						<div
							@click="showFormDetail(itemData[tableId])"
							class="table__update"
						>
							{{ $resourcesVN.TABLE.Watch }}
						</div>
						<div
							v-on="
								contextMenuListeners(
									contextFunction(itemData['is_mention'], itemData[tableId])
								)
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
		<tfoot>
			<tr>
				<th v-for="(item, index) in tableStyle.length + 2" :key="index">
					{{ footerTable(index) }}
				</th>
			</tr>
		</tfoot>
	</table>
</template>
<script>
	// LIBRARY
	import voucherAPI from "../../../js/components/voucherAPI";
	import methods from "../../../mixins/methods";
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
	import TableDataStyle from "../../../js/enum/tableDataStyle.js";

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
			totalPrices: {
				type: Number,
				default: 0,
			},
			contextFunction: {
				type: Function,
				default: function() {
					return [];
				},
			},
			tableId: {
				type: String,
				default: "",
			},
		},
		data() {
			return {
				currId: null,
				checkAll: false,
				markRows: [],
			};
		},	
		methods: {
			/**
			 * Đặt kiểu cho ô dữ liệu
			 * @param {Object} itemStyle kiểu được lưu trong object
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			recordStyle(itemStyle) {
				var styleClass = { "table--loading": this.tableLoading };
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
				} else if (field.includes("price")) {
					formatedValue = this.formatMoney(value);
				} else if (field.includes("type")) {
					switch (Number(value)) {
						case this.$enum.VOUCHER_TYPE.One:
							formatedValue = "Thành phần sản xuất";
							break;
						case this.$enum.VOUCHER_TYPE.Two:
							formatedValue = "Hàng bán bị trả lại";
							break;
						case this.$enum.VOUCHER_TYPE.Three:
							formatedValue = " Khác (NVL thừa, HH thuê gia công, ...)";
							break;
					}
				} else {
					formatedValue = value;
				}
				return formatedValue;
			},
			/**
			 * Ấn vào hàng ở bảng
			 * @param {Number} index
			 * @param {String} tata
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			choose(index, data) {
				if (index == 1) {
					this.showFormDetail(data);
				}
			},
			showFormDetail(id) {
				this.currId = id;
				this.$bus.$emit("showLoading");
				voucherAPI
					.getVoucherDetail(id)
					.then((res) => {
						this.$bus.$emit("showWarehouseDetail", {
							mode: this.$enum.FORM_MODE.Update,
							data: res.data.Data,
						});
						this.$bus.$emit("hideLoading");
					})
					.catch((res) => {
						this.$bus.$emit("hideLoading");
						console.log(res);
					});
			},
			footerTable(index) {
				if (index == 1) {
					return "Tổng";
				}
				if (index == 4) {
					return this.formatMoney(this.totalPrices);
				}
			},
		},
		watch: {
			/**
			 * Khi trạng thái checkbox thay đổi thì emit ra ngoài
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			markRows: {
				handler(newValue) {
					this.$emit("input", newValue);
				},
				deep: true,
			},
			/**
			 * Khi check và bỏ check tất cả
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			checkAll: function(state) {
				for (var i = 0; i < this.tableData.length; i++) {
					this.$set(this.markRows, i, state);
				}
			},
			/**
			 * Khi table load lại thì bỏ hết check
			 * @param {Boolean} state
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			tableLoading: function(state) {
				if (state) {
					for (var i = 0; i < this.tableData.length; i++) {
						this.$set(this.markRows, i, false);
					}
				}
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/table/table.css");
</style>
