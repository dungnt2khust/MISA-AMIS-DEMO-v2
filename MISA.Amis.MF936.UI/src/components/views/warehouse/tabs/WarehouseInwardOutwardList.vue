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
		<div class="table-wrapper">
			<div class="table__control">
				<div class="fx">
					<base-many-control class="mr-8" />
					<base-filter />
				</div>
				<base-function />
			</div>
			<base-table
				:tableStyle="WAREHOUSE_TABLE['InwardOutwardList']"
				:tableData="tableData"
				:tableLoading="tableLoading"
				:totalPrices="totalPrices"
			/>
			<base-pagination
				:totalPage="totalPage"
				:totalRecord="totalRecord"
				:currPage="currPage"
				:currOption="currOption"
				:paginationData="PAGINATION_DATA['Normal']"
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
		},
		props: {
			warehouseDetailState: {
				type: Boolean,
				default: false,
			},
		},
		data() {
			return {
				inputTimeout: null,
				filterString: "",
				tableData: [],
				tableLoading: false,
				totalPage: 0,
				totalRecord: 0,
				currPage: 1,
				currOption: 0,
				totalPrices: 0
			};
		},
		mounted() {
			/**
			 * Load dữ liệu khi mở trang lên
			 * CreatedBy: NTDUNG (24/09/2021)
			 */
			this.getData();
		},
		methods: {
			/**
			 * Lấy dữ liệu cho bảng
			 * CreatedBy: NTDUNG (24/09/2021)
			 */
			getData() {
				voucherAPI
					.getAccountVoucherPagingFilter("", "", "", "", "", 1, 20)
					.then((res) => {
						console.log(res);
						this.tableData = res.data['Vouchers'];
						this.totalPage = res.data['TotalPage'];
						this.totalRecord = res.data['TotalRecord'];
						this.totalPrices = res.data['TotalPrices'];
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
				this.$bus.$emit("showWarehouseDetail");
			},
		},
	};
</script>
<style lang=""></style>
