<template lang="">
	<div>
		<base-form-large
			:title="'Phiếu nhập kho ' + masterContent['voucher_code']"
			v-model="formLargeState"
			:formLargeState="formLargeState"
		>
			<template v-slot:header>
				<div class="formlarge__type ml-40">
					<base-combobox
						:comboboxData="voucherTypeData"
						:currIdx="voucherIdx"
						v-model="voucherIdx"
						class="mr-16"
					/>
				</div>
			</template>

			<template v-slot:body>
				<div class="formlarge__body-part1 fx-wrap">
					<div class="formlarge__body-main-info w-3/4">
						<div class="w-4/5 fx-wrap pr-10 border-right">
							<div class="fx-3/7 mb-10">
								<base-combobox-advance :tabindex="0" label="Khách hàng" />
							</div>
							<div class="fx-4/7 mb-10">
								<base-input
									:value="masterContent['contact_address']"
									v-model="masterContent['contact_address']"
									:tabindex="1"
									label="Địa chỉ"
								/>
							</div>
							<div class="fx-3/7 mb-10">
								<base-input
									:value="masterContent['contact_name']"
									v-model="masterContent['contact_name']"
									:tabindex="2"
									label="Người giao hàng"
								/>
							</div>
							<div class="fx-4/7 mb-10">
								<base-input
									:value="masterContent['description']"
									v-model="masterContent['description']"
									:tabindex="3"
									label="Diễn giải"
								/>
							</div>
							<div class="fx-item mb-10">
								<base-input
									label="Kèm theo"
									unit="chứng từ gốc"
									width="200px"
									:value="masterContent['voucher_code']"
									:tabindex="4"
								/>
							</div>
						</div>
						<div class="w-1/5 ml-16">
							<div class="fx-100 mb-10">
								<base-input-date
									:value="masterContent['mathematics_date']"
									v-model="masterContent['mathematics_date']"
									:tabindex="5"
									label="Ngày hạch toán"
								/>
							</div>
							<div class="fx-100 mb-10">
								<base-input-date
									:value="masterContent['voucher_date']"
									v-model="masterContent['voucher_date']"
									:tabindex="6"
									label="Ngày chứng từ"
								/>
							</div>
							<div class="fx-100 mb-10">
								<base-input
									:value="masterContent['voucher_code']"
									v-model="masterContent['voucher_code']"
									:tabindex="7"
									label="Số chứng từ"
								/>
							</div>
						</div>
					</div>
					<div class="formlarge__body-cash w-1/4">
						<div class="formlarge__body-cash-title">Tổng tiền</div>
						<h1 class="formlarge__body-cash-number">
							{{ formatMoney(masterContent["total_price"]) }}
						</h1>
					</div>
					<div class="formlarge__reference w-2/5 mb-20 mt-20">
						Tham chiếu
						<div @click="reference()" class="formlarge__reference-icon"></div>
					</div>
				</div>
				<!-- Escap -->
				<div class="formlarge__body-part2">
					<base-table-input
						:tableData="tableInputData"
						:tableStyle="tableInputStyle"
						v-model="tableInputData"
					/>
				</div>
			</template>
		</base-form-large>
		<warehouse-select-commodity
			v-if="selectCommodityState"
			v-model="selectCommodityState"
		/>
		<warehouse-add width="500px" v-if="false" />
		<warehouse-add-commodity-group width="600px" v-if="false" />
		<warehouse-add-unit width="500px" v-if="false" />
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import TableDataStyle from "../../../../js/enum/tableDataStyle";
	import VoucherDetailState from "../../../../js/enum/voucherDetailState";
	import methods from "../../../../mixins/methods.js";
	// COMPONENTS
	import BaseFormLarge from "../../../Base/Form/BaseFormLarge.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseInputDate from "../../../Base/BaseInputDate.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseCombobox from "../../../Base/Select/BaseCombobox.vue";
	import BaseTableInput from "../../../Base/Table/BaseTableInput.vue";
	import WarehouseSelectCommodity from "../formextend/WarehouseSelectCommodity.vue";
	import WarehouseAdd from "../formsmall/WarehouseAdd.vue";
	import WarehouseAddCommodityGroup from "../formsmall/WarehouseAddCommodityGroup.vue";
	import WarehouseAddUnit from "../formsmall/WarehouseAddUnit.vue";

	export default {
		name: "WarehouseDetail",
		mixins: [globalComponents, methods],
		components: {
			BaseFormLarge,
			BaseInput,
			BaseInputDate,
			BaseComboboxAdvance,
			BaseCombobox,
			BaseTableInput,
			WarehouseSelectCommodity,
			WarehouseAdd,
			WarehouseAddCommodityGroup,
			WarehouseAddUnit,
		},
		data() {
			return {
				formLargeState: false,
				selectCommodityState: false,
				createCommodityState: false,
				masterContent: {},
				tableInputData: [],
				tableInputStyle: [
					{
						name: "Mã hàng",
						field: "commodity_code",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
					{
						name: "Tên hàng",
						field: "commodity_name",
						width: "300px",
						type: TableDataStyle.TYPE.Input,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
					{
						name: "Kho",
						field: "warehouse_code",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						api: this.$resourcesVN.API.Warehouse,
						enable: true,
					},
					{
						name: "TK nợ",
						field: "credit_account_number",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
					{
						name: "TK có",
						field: "debit_account_number",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
					{
						name: "ĐVT",
						field: "units",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
					{
						name: "Số lượng",
						field: "quantity",
						width: "150px",
						type: TableDataStyle.TYPE.Input,
						pos: TableDataStyle.POS.Right,
						enable: true,
					},
					{
						name: "Đơn giá",
						field: "debit_amount",
						width: "150px",
						type: TableDataStyle.TYPE.Input,
						pos: TableDataStyle.POS.Right,
						enable: true,
					},
					{
						name: "Thành tiền",
						field: "total_price",
						width: "100px",
						type: TableDataStyle.TYPE.Input,
						pos: TableDataStyle.POS.Right,
						enable: true,
					},
					{
						name: "Hạn sử dụng",
						field: "expiry",
						width: "150px",
						type: TableDataStyle.TYPE.InputDate,
						pos: TableDataStyle.POS.Left,
						enable: true,
					},
				],
				voucherIdx: 1,
				voucherTypeData: [
					{ name: "1. Thành phẩm sản xuất" },
					{ name: "2. Hàng bán bị trả lại" },
					{ name: "3. Khác (NVL thừa, HH thuê gia công, ...)" },
				],
			};
		},
		created() {
			this.$bus.$on("showWarehouseDetail", (data) => {
				this.formLargeState = true;
				if (data["mode"] == "UPDATE") {
					this.masterContent = data["data"]["in_inward"][0];
					var dataDetail = data["data"]["in_inward_detail"];
					this.tableInputData = [];
					dataDetail.forEach((item, index) => {
						this.$set(this.tableInputData, index, item["voucher_detail"]);
						this.$set(this.tableInputData[index], "units", item["units"]);
						this.$set(
							this.tableInputData[index],
							"state",
							VoucherDetailState.UPDATE
						);
					});
				}
				if (data['mode'] == "ADD") {
					this.masterContent = {};
					this.tableInputData = [];
					this.masterContent['description'] = 'Nhập kho từ '+ this.voucherTypeData[this.voucherIdx]['name'].substring(3);
				}
			});
			this.$bus.$on("hideWarehouseDetail", () => {
				this.formLargeState = false;
			});
		},
		methods: {
			/**
			 * Tham chiếU chứng từ
			 * CreatedBy: NTDUNG (28/09/2021)
			 */
			reference() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
		},
		watch: {
			/**
			 * Khi đổi loại chứng từ thì thay đổi mô tả
			 * @param {Number} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			voucherIdx: function(value) {
				this.masterContent['description'] = 'Nhập kho từ '+ this.voucherTypeData[value]['name'].substring(3);
			}
		},
		destroyed() {
			this.$bus.$off("showWarehouseDetail");
			this.$bus.$off("hideWarehouseDetail");
		},
	};
</script>
<style lang=""></style>
