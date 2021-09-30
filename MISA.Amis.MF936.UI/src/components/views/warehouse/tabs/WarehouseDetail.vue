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
								<base-combobox-advance
									:api="WAREHOUSE_TABLE.InWardDetail.OBJECT['api']"
									:controller="
										WAREHOUSE_TABLE.InWardDetail.OBJECT['controller']
									"
									v-model="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']]
									"
									:valueBind="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']]
									"
									:vmodelField="WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']"
									:field="WAREHOUSE_TABLE.InWardDetail.OBJECT['field']"
									:default="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['field']]
									"
									:listGridStyle="WAREHOUSE_TABLE.InWardDetail.OBJECT['style']"
									:form="WAREHOUSE_TABLE.InWardDetail.OBJECT['form']"
									:tabindex="0"
									label="Đối tượng"
								/>
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
							<div class="fx-3/7">
								<base-combobox-advance
									:api="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['api']"
									:controller="
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['controller']
									"
									v-model="
										masterContent[
											WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']
										]
									"
									:valueBind="
										masterContent[
											WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']
										]
									"
									:vmodelField="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']"
									:field="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['field']"
									:default="
										masterContent[
											WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['field']
										]
									"
									:listGridStyle="
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['style']
									"
									:form="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['form']"
									:tabindex="0"
									label="Nhân viên"
								/>
							</div>
							<div class="fx-4/7 mb-10">
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
						:tableStyle="WAREHOUSE_TABLE.InWardDetail.TABLE_INPUT"
						v-model="tableInputData"
					/>
				</div>
			</template>
			<template v-slot:footer>
				<base-button
					:label="$resourcesVN.FORM.Cancel"
					:method="hideForm"
					type="dark"
				/>
				<div class="formlarge__store fx">
					<base-button
						@click.native="store()"
						:label="$resourcesVN.FORM.Store"
						type="dark"
						class="mr-8"
					/>
					<base-button :label="$resourcesVN.FORM.StoreAndPrint" type="green" />
				</div>
			</template>
		</base-form-large>
		<warehouse-select-commodity />
		<warehouse-add width="500px" />
		<warehouse-add-commodity-group width="600px" />
		<warehouse-add-unit width="500px" />
	</div>
</template>
<script>
	// LIBRARY
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import VoucherDetailState from "../../../../js/enum/voucherDetailState";
	import methods from "../../../../mixins/methods.js";
	// import voucherAPI from "../../../../js/components/voucherAPI"
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
	import BaseButton from "../../../Base/Button/BaseButton.vue";

	export default {
		name: "WarehouseDetail",
		mixins: [globalComponents, methods, warehouseTable],
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
			BaseButton,
		},
		data() {
			return {
				formLargeState: false,
				masterContent: {},
				tableInputData: [],
				units: [],
				mode: null,
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
					// this.mode = ''
					this.masterContent = data["data"]["in_inward"][0];
					var dataDetail = data["data"]["in_inward_detail"];
					this.tableInputData = [];
					dataDetail.forEach((item, index) => {
						this.$set(this.tableInputData, index, item["voucher_detail"]);
						this.$set(this.tableInputData[index], "units", item["units"]);
						this.units[index] = item["units"];
						var mainUnit = this.tableInputData[index]["units"].find((item) => {
							return item["is_main_unit"] == 1;
						});

						this.$set(
							this.tableInputData[index],
							"mainUnitId",
							mainUnit["unit_id"]
						);

						this.$set(
							this.tableInputData[index],
							"main_unit_name",
							mainUnit["unit_name"]
						);

						this.$set(
							this.tableInputData[index],
							"main_unit_rate",
							mainUnit["rate"]
						);

						this.$set(
							this.tableInputData[index],
							"state",
							VoucherDetailState.UPDATE
						);
					});
				}
				if (data["mode"] == "ADD") {
					this.masterContent = {};
					this.tableInputData = [];
					this.masterContent["description"] =
						"Nhập kho từ " +
						this.voucherTypeData[this.voucherIdx]["name"].substring(3);
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
			/**
			 * Tắt form
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			hideForm() {
				this.formLargeState = false;
			},
			/**
			 * Cất dữ liệu
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			store() {
				console.log(JSON.stringify({
					in_inward: this.masterContent,
					in_inward_detail: this.tableInputData,
				}));
				// voucherAPI.
			},
		},
		watch: {
			/**
			 * Khi đổi loại chứng từ thì thay đổi mô tả
			 * @param {Number} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			voucherIdx: function(value) {
				this.masterContent["description"] =
					"Nhập kho từ " + this.voucherTypeData[value]["name"].substring(3);
			},
		},
		destroyed() {
			this.$bus.$off("showWarehouseDetail");
			this.$bus.$off("hideWarehouseDetail");
		},
	};
</script>
<style lang=""></style>
