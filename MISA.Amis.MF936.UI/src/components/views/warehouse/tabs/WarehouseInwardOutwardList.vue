<template lang="">
	<div id="warehouse">
		<div class="content__header">
			<div class="content__name">
				{{ WAREHOUSE_TABLE["InwardOutwardListTitle"] }}
			</div>
			<div class="content__control">
				<base-tutorial />
				<div class="content__function">
					<div
						@click="addOnClick()"
						class="content__add button button--green button--half-left"
					>
						{{ $resourcesVN.WAREHOUSE_ADD }}
					</div>
					<div
						class="content__import button button--green button--half-right"
					></div>
				</div>
			</div>
		</div>
		<div class="table-wrapper">
			<div class="table__control">
				<div class="fx">
					<base-many-control style="margin-right: 8px;" />
					<base-filter />
				</div>
				<base-function />
			</div>
			<base-table
				:tableStyle="WAREHOUSE_TABLE['InwardOutwardList']"
				:tableData="tableData"
				:tableLoading="tableLoading"
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

	// COMPONENT
	import BaseTable from "../../../Base/BaseTable.vue";
	import BasePagination from "../../../Base/BasePagination.vue";
	import BaseManyControl from "../../../Base/BaseManyControl.vue";
	import BaseFunction from "../../../Base/BaseFunction.vue";
	import BaseTutorial from "../../../Base/BaseTutorial.vue";
	import BaseFilter from "../../../Base/BaseFilter.vue";

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
		},
		props: {
			warehouseDetailState: {
				type: Boolean,
				default: false
			}
		},
		data() {
			return {
				inputTimeout: null,
				filterString: "",
				tableData: [
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
					{
						mathematics_date: "121341241",
						voucher_code: "fasfasf",
						description: "fas",
						summary_money: "faslfhlasfj",
						account_object_name: "fjas;dfja",
						is_mention: "faslfjas",
						voucher_type: "24123412412",
					},
				],
				tableLoading: false,
				totalPage: 0,
				totalRecord: 0,
				currPage: 1,
				currOption: 0,
			};
		},
		methods: {
			/**
			 * Ấn vào nút thêm mới
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			addOnClick() {
				this.$bus.$emit('showWarehouseDetail');
			},
		},
	};
</script>
<style lang=""></style>
