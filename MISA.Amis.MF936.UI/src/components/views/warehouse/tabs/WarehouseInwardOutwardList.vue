<template lang="">
	<div id="warehouse">
		<div class="content__header">
			<div class="content__name">
				{{ WAREHOUSE_TABLE["InwardOutwardListTitle"] }}
			</div>
			<div class="fx">
				<base-tutorial class="mr-20" />
				<base-button-double
					:label="$resourcesVN.WAREHOUSE_ADD"
					:method="addRecord"
				/>
			</div>
		</div>
		<base-tab2 :listTab="listTab" class="mt-20" />
		<div class="table-wrapper">
			<div class="table__control">
				<div class="fx">
					<base-many-control
						class="mr-8"
						:listManyControl="listManyControl"
						:enable="tableSelectMany"
					/>
					<base-filter v-model="filterData" />
				</div>
				<base-function v-model="functionChose" />
			</div>
			<base-table
				:tableId="tableId"
				:tableStyle="WAREHOUSE_TABLE['InwardOutwardList']"
				:tableData="tableData"
				:tableLoading="tableLoading"
				:totalPrices="totalPrices"
				:contextFunction="contextFunction"
				v-model="markRows"
			/>
			<base-pagination
				:totalPage="totalPage"
				:totalRecord="totalRecord"
				:currPage="currPage"
				:currOption="currOption"
				:paginationData="PAGINATION_DATA['Normal']"
				@changePageInfo="changePageInfo($event)"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import pagination from "../../../../mixins/pagination.js";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import methods from "../../../../mixins/methods.js";
	import voucherAPI from "../../../../js/components/voucherAPI";

	// COMPONENT
	import BaseTable from "../../../Base/Table/BaseTable.vue";
	import BasePagination from "../../../Base/BasePagination.vue";
	import BaseManyControl from "../../../Base/BaseManyControl.vue";
	import BaseFunction from "../../../Base/BaseFunction.vue";
	import BaseTutorial from "../../../Base/BaseTutorial.vue";
	import BaseFilter from "../../../Base/BaseFilter.vue";
	import BaseButtonDouble from "../../../Base/Button/BaseButtonDouble.vue";
	import BaseTab2 from "../../../Base/Tab/BaseTab2.vue";

	export default {
		name: "WarehouseInwardOutwardList",
		mixins: [warehouseTable, pagination, globalComponents, methods],
		components: {
			BaseTable,
			BasePagination,
			BaseManyControl,
			BaseFunction,
			BaseTutorial,
			BaseFilter,
			BaseButtonDouble,
			BaseTab2,
		},
		props: {
			warehouseDetailState: {
				type: Boolean,
				default: false,
			},
		},
		data() {
			return {
				markRows: [],
				tableData: [],
				tableId: "accountvoucher_id",
				tableSelectMany: false,
				tableLoading: false,
				totalPage: 0,
				totalRecord: 0,
				totalPrices: 0,
				// FUNCTION
				functionChose: {},
				// FILTER
				filterData: "",
				searchData: "",
				mentionState: "",
				voucherType: "",
				startDate: "",
				endDate: "",
				currPage: 1,
				currOption: 0,
				// SUB TABS
				listTab: [
					{ name: "Tất cả" },
					{ name: "Nhập kho" },
					{ name: "Xuất kho" },
					{ name: "Chuyển kho" },
				],
			};
		},
		computed: {
			/**
			 * Những dòng đã được chọn
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			selectedRows() {
				var rows = [];
				this.markRows.forEach((row, index) => {
					if (row) rows.push(this.tableData[index][this.tableId]);
				});
				return rows;
			},
			/**
			 * Các chức năng hàng loạt
			 * @return {Array}
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			listManyControl() {
				return [
					{
						name: "Ghi sổ",
						function: () => {
							this.mention();
						},
					},
					{
						name: "Bỏ ghi",
						function: () => {
							this.unMention();
						},
					},
					{
						name: "Xoá",
						function: () => {
							this.delete();
						},
					},
				];
			},
		},
		methods: {
			/**
			 * Chức năng trên bảng dữ liệu
			 * @param {Number} value
			 * @param {String} id
			 * @return {Array}
			 */
			contextFunction(value, id) {
				if (value)
					return [
						{
							name: "Bỏ ghi",
							function: () => {
								this.unMention(id);
							},
						},
						{
							name: "Nhân bản",
							function: () => {
								this.replication(id);
							},
						},
					];
				else
					return [
						{
							name: "Ghi sổ",
							function: () => {
								this.mention(id);
							},
						},
						{
							name: "Xoá",
							function: () => {
								this.delete(id);
							},
						},
						{
							name: "Nhân bản",
							function: () => {
								this.replication(id);
							},
						},
					];
			},
			/**
			 * Lấy dữ liệu cho bảng
			 * CreatedBy: NTDUNG (24/09/2021)
			 */
			getData() {
				this.tableLoading = true;
				voucherAPI
					.getAccountVoucherPagingFilter(
						this.searchData,
						this.mentionState,
						this.voucherType,
						this.startDate,
						this.endDate,
						this.currPage,
						this.PAGINATION_DATA["Normal"][this.currOption]["PaginationId"]
					)
					.then((res) => {
						console.log(res);
						this.tableData = res.data["Vouchers"];
						this.totalPage = res.data["TotalPage"];
						this.totalRecord = res.data["TotalRecord"];
						this.totalPrices = res.data["TotalPrices"];
						this.tableLoading = false;
					})
					.catch((res) => {
						console.log(res);
					});
			},
			/**
			 * Ấn vào nút thêm mới
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			addRecord() {
				this.$bus.$emit("showWarehouseDetail", {mode: 'ADD'});
			},
			/**
			 * Xử lý khi pagination thay đổi
			 * @param {Object} newInfo
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			changePageInfo(newInfo) {
				this.currPage = newInfo["currPage"];
				this.currOption = newInfo["currOption"];
				this.getData();
			},
			/**
			 * Ghi sổ một hoặc nhiều
			 * @param {String} id ghi sổ 1 thì truyền id, không truyền thì ghi sổ nhiều
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			mention(id) {
				if (id) {
					var data = { ...this.findDataWidthId(id) };
					data["is_mention"] = 1;
					console.log(data);
					voucherAPI
						.put(id, data)
						.then(() => this.getData())
						.catch((res) => console.log(res));
				} else {
					voucherAPI
						.mentionMany(this.selectedRows)
						.then(() => {
							this.getData();
						})
						.catch((res) => console.log(res));
				}
			},
			/*
			 * Bỏ ghi một hoặc nhiều
			 * @param {String} id bỏ ghi thì truyền id, không truyền thì bỏ ghi nhiều
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			unMention(id) {
				if (id) {
					var data = { ...this.findDataWidthId(id) };
					data["is_mention"] = 0;
					voucherAPI
						.put(id, data)
						.then(() => this.getData())
						.catch((res) => console.log(res));
				} else {
					voucherAPI
						.unMentionMany(this.selectedRows)
						.then(() => {
							this.getData();
						})
						.catch((res) => console.log(res));
				}
			},
			/**
			 * Xoá một hoặc nhiều
			 * @param {String} id Xoá 1 thì truyền id, không truyền thì xoá nhiều
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			delete(id) {
				if (id) {
					var data = { ...this.findDataWidthId(id) };
					this.callDialog(
						"confirm",
						`Bạn có muốn xoá ${data["voucher_code"]} ?`
					).then((answer) => {
						if (answer == "YES") {
							voucherAPI
								.delete(id)
								.then(() => {
									this.getData();
								})
								.catch((res) => console.log(res));
						}
					});
				} else {
					this.callDialog("confirm", `Bạn có muốn xoá không ?`).then(
						(answer) => {
							if (answer == "YES") {
								voucherAPI
									.deleteMany(this.selectedRows)
									.then(() => {
										this.getData();
									})
									.catch((res) => console.log(res));
							}
						}
					);
				}
			},
			/**
			 * Nhân bản theo id
			 * @param {String} id
			 *  CreatedBy: NTDUNG (28/09/2021)
			 */	
			replication(id) {
				console.log('Nhân bản', id);
				this.callDialog('warn', this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Trả về data theo id
			 * @param {String} id
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			findDataWidthId(id) {
				var foundIdx = this.tableData.findIndex((item) => {
					return item[this.tableId] == id;
				});
				return this.tableData[foundIdx];
			},
		},
		watch: {
			/**
			 * Khi chức năng được lựa chọn thì thực hiện
			 * @param {Array} value
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			functionChose: {
				handler(value) {
					switch (value["name"]) {
						case "Reload":
							this.getData();
							break;
						case "Export":
							this.callDialog(
								"warn",
								this.$resourcesVN.NOTIFY.FeatureNotAvaiable
							);
							break;
						case "Search":
							this.searchData = value["data"];
							this.currPage = 1;
							this.getData();
							break;
					}
				},
				deep: true,
			},
			/**
			 * Khi dữ liệu lọc thay đổi thì tải lại dữ liệu
			 * @param {Object} value
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			filterData: function(value) {
				this.mentionState = value["mention"];
				this.startDate = value["formDate"];
				this.endDate = value["toDate"];
				this.currPage = 1;
				this.getData();
			},
			/**
			 * Khi số dòng nhỏ hơn 2 thì ẩn đi
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			markRows: function() {
				if (this.selectedRows.length < 2) this.tableSelectMany = false;
				else this.tableSelectMany = true;
			},
		},
	};
</script>
<style lang=""></style>
