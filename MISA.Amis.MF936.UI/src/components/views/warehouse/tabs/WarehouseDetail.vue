<template lang="">
	<!-- v-on="
			shortKeysListeners([
				{
					keys: ['Control', 'e'],
					function: () => {
						if (!this.enable) this.edit();
					},
				},
			])
		" -->
	<!-- eslint-disable -->
	<div
		tabindex="0"
		v-on:keydown.ctrl.83.stop.prevent="store"
		v-on:keydown.ctrl.alt.80.stop.prevent="storeAndPrint"
		v-on:keydown.ctrl.71.stop.prevent="mention"
		v-on:keydown.ctrl.66.stop.prevent="unMention"
		v-on:keydown.27.prevent="hideForm"
		v-if="formLargeState"
		class="warehousedetail"
	>
		<!-- eslint-enable -->
		<base-form-large
			:title="
				formatString(
					$resourcesVN.WAREHOUSE_DETAIL.Title,
					masterContent['voucher_code']
				)
			"
			@hideForm="hideForm()"
		>
			<template v-slot:header>
				<div class="formlarge__type ml-40">
					<base-combobox
						:comboboxData="voucherTypeData"
						:currIdx="masterContent['voucher_type']"
						:enable="enable"
						v-model="masterContent['voucher_type']"
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
									:enable="enable"
									:controller="
										WAREHOUSE_TABLE.InWardDetail.OBJECT['controller']
									"
									v-model="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']]
									"
									:value="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']]
									"
									:vmodelField="WAREHOUSE_TABLE.InWardDetail.OBJECT['vmodel']"
									:field="WAREHOUSE_TABLE.InWardDetail.OBJECT['field']"
									:default="
										masterContent[WAREHOUSE_TABLE.InWardDetail.OBJECT['field']]
									"
									:listGridStyle="WAREHOUSE_TABLE.InWardDetail.OBJECT['style']"
									:form="WAREHOUSE_TABLE.InWardDetail.OBJECT['form']"
									:label="$resourcesVN.WAREHOUSE_DETAIL.Object"
									:formName="name"
									syncfield="AccountObject"
									ref="inputFocus"
								/>
							</div>
							<div class="fx-4/7 mb-10">
								<base-input
									:value="masterContent['contact_address']"
									v-model="masterContent['contact_address']"
									:enable="enable"
									:label="$resourcesVN.WAREHOUSE_DETAIL.Address"
								/>
							</div>
							<div class="fx-3/7 mb-10">
								<base-input
									:value="masterContent['contact_name']"
									v-model="masterContent['contact_name']"
									:enable="enable"
									:label="$resourcesVN.WAREHOUSE_DETAIL.Shipper"
								/>
							</div>
							<div class="fx-4/7 mb-10">
								<base-input
									:value="masterContent['description']"
									v-model="masterContent['description']"
									:enable="enable"
									:label="$resourcesVN.WAREHOUSE_DETAIL.Description"
								/>
							</div>
							<div class="fx-3/7">
								<base-combobox-advance
									:enable="enable"
									:controller="
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['controller']
									"
									v-model="
										masterContent[
											WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']
										]
									"
									:value="
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
									label="Nhân viên"
									:formName="name"
								/>
							</div>
							<div class="fx-4/7 mb-10">
								<base-input
									:label="$resourcesVN.WAREHOUSE_DETAIL.Attach"
									:unit="$resourcesVN.WAREHOUSE_DETAIL.OriginalVoucher"
									width="200px"
									:value="masterContent['voucher_attach']"
									v-model="masterContent['voucher_attach']"
									placeholder="Số lượng"
									:enable="enable"
									:formName="name"
								/>
							</div>
						</div>
						<div class="w-1/5 ml-16">
							<div class="fx-1 mb-10">
								<base-input-date
									:value="masterContent['mathematics_date']"
									v-model="masterContent['mathematics_date']"
									:label="$resourcesVN.WAREHOUSE_DETAIL.MathematicsDate"
									:enable="enable"
									:required="true"
									:formName="name"
									:range="{
										from: {
											Date: masterContent['voucher_date'],
											Field: $resourcesVN.WAREHOUSE_DETAIL.VoucherDate,
										},
									}"
									:hasDefault="true"
								/>
							</div>
							<div class="fx-1 mb-10">
								<base-input-date
									:value="masterContent['voucher_date']"
									v-model="masterContent['voucher_date']"
									:label="$resourcesVN.WAREHOUSE_DETAIL.VoucherDate"
									:enable="enable"
									:formName="name"
									:required="true"
									:hasDefault="true"
								/>
							</div>
							<div class="fx-1 mb-10">
								<base-input
									:value="masterContent['voucher_code']"
									v-model="masterContent['voucher_code']"
									:label="$resourcesVN.WAREHOUSE_DETAIL.VoucherCode"
									:enable="enable"
									:formName="name"
									:required="true"
								/>
							</div>
						</div>
					</div>
					<div class="formlarge__body-cash w-1/4">
						<div class="formlarge__body-cash-title">
							{{ $resourcesVN.WAREHOUSE_DETAIL.TotalPrice }}
						</div>
						<h1 class="formlarge__body-cash-number">
							{{ formatMoney(masterContent["total_price"]) }}
						</h1>
					</div>
					<div class="formlarge__reference w-2/5 mb-20 mt-20">
						{{ $resourcesVN.WAREHOUSE_DETAIL.Reference }}
						<div @click="reference()" class="formlarge__reference-icon"></div>
					</div>
				</div>
				<!-- Escap -->
				<div class="formlarge__body-part2">
					<base-table-input
						:defaultBind="{
							name: 'accountvoucher_id',
							value: masterContent['accountvoucher_id'],
						}"
						:tableData="tableInputData"
						:tableStyle="WAREHOUSE_TABLE.InWardDetail.TABLE_INPUT"
						v-model="tableInputData"
						:enable="enable"
						:formName="name"
						:formMode="mode"
					/>
				</div>
			</template>
			<template v-slot:footer>
				<base-button
					:label="$resourcesVN.FORM.Close.Label"
					:tooltip="$resourcesVN.FORM.Close.Tooltip"
					:method="hideForm"
					type="dark"
				/>
				<div class="formlarge__store fx">
					<base-button
						@click.native="store()"
						:label="$resourcesVN.FORM.Store.Label"
						:tooltip="$resourcesVN.FORM.Store.Tooltip"
						v-if="enable"
						type="dark"
						class="mr-8"
					/>
					<base-button
						@click.native="storeAndPrint()"
						:label="$resourcesVN.FORM.StoreAndPrint.Label"
						:tooltip="$resourcesVN.FORM.StoreAndPrint.Tooltip"
						v-if="enable"
						type="green"
					/>
					<base-button
						class="mr-8"
						@click.native="edit()"
						:label="$resourcesVN.FORM.Edit.Label"
						:tooltip="$resourcesVN.FORM.Edit.Tooltip"
						type="dark"
						v-if="!masterContent['is_mention'] && !enable"
					/>
					<base-button
						@click.native="unMention()"
						:label="$resourcesVN.FORM.UnMention.Label"
						:tooltip="$resourcesVN.FORM.UnMention.Tooltip"
						type="green"
						v-if="masterContent['is_mention'] && !enable"
					/>
					<base-button
						@click.native="mention()"
						:label="$resourcesVN.FORM.Mention.Label"
						:tooltip="$resourcesVN.FORM.Mention.Tooltip"
						type="green"
						v-if="!masterContent['is_mention'] && !enable"
					/>
				</div>
			</template>
		</base-form-large>
		<warehouse-select-commodity />
		<warehouse-add width="500px" />
		<warehouse-add-commodity-group width="600px" />
		<warehouse-add-unit width="500px" />
		<base-list-grid />
	</div>
</template>
<script>
	// LIBRARY
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import VoucherDetailState from "../../../../js/enum/voucherDetailState";
	import methods from "../../../../mixins/methods.js";
	import baseAPI from "../../../../js/base/baseAPI.js";
	import voucherAPI from "../../../../js/components/voucherAPI.js";
	import shortKeys from "../../../../mixins/globalComponents/shortKeys";
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
	import BaseListGrid from "../../../Base/Select/BaseListGrid.vue";

	export default {
		name: "WarehouseDetail",
		mixins: [globalComponents, methods, warehouseTable, shortKeys],
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
			BaseListGrid,
		},
		data() {
			return {
				formLargeState: false,
				masterContent: {},
				tableInputData: [],
				dataClone: [],
				units: [],
				mode: null,
				enable: null,
				needReload: false,
				voucherTypeData: [
					{ name: "1. Thành phẩm sản xuất" },
					{ name: "2. Hàng bán bị trả lại" },
					{ name: "3. Khác (NVL thừa, HH thuê gia công, ...)" },
				],
				errorMsg: "",
				element: null,
				name: "Voucher",
				baseAPI: new baseAPI("AccountVouchers"),
				intervalInput: null,
			};
		},
		created() {
			/**
			 * Bắt lỗi trả về từ các components
			 * @param {String} msg: Nội dung lỗi
			 * @param {Element} element: Component lỗi
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("catchError" + this.name, (msg, element) => {
				if (!this.errorMsg && element) {
					this.errorMsg = msg;
					this.element = element;
				}
			});

			/**
			 * Sự kiện bật form
			 * @param {Object} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("showWarehouseDetail", (data) => {
				this.formLargeState = true;

				switch (data["mode"]) {
					case this.$enum.FORM_MODE.Update:
						this.enable = false;
						this.mode = this.$enum.FORM_MODE.Update;
						this.masterContent = data["data"]["in_inward"][0];

						var dataDetail = data["data"]["in_inward_detail"];
						this.tableInputData = [];
						dataDetail.forEach((item, index) => {
							this.$set(this.tableInputData, index, item["voucher_detail"]);
							this.units[index] = item["voucher_detail"]["units"];
							var mainUnit = this.tableInputData[index]["units"].find(
								(item) => {
									return item["is_main_unit"] == 1;
								}
							);
							var selectedUnitId = item["voucher_detail"]["selected_unit_id"];
							var selectedUnit = item["voucher_detail"]["units"].find(
								(item) => {
									return item["unit_id"] == selectedUnitId;
								}
							);
							this.$set(
								this.tableInputData[index],
								"main_unit_rate",
								mainUnit["rate"]
							);
							this.$set(
								this.tableInputData[index],
								"main_unit_id",
								mainUnit["unit_id"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_id",
								selectedUnit["unit_id"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_name",
								selectedUnit["unit_name"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_rate",
								selectedUnit["rate"]
							);

							this.$set(
								this.tableInputData[index],
								"state",
								VoucherDetailState.UPDATE
							);
						});
						this.cloneData();
						break;
					case this.$enum.FORM_MODE.Add:
						this.enable = true;
						this.mode = this.$enum.FORM_MODE.Add;
						this.masterContent = {};
						this.tableInputData = [];
						this.$bus.$emit("showLoading");
						this.baseAPI
							.getNewCode()
							.then((res) => {
								console.log(res);
								this.$set(this.masterContent, "voucher_code", res.data);
								setTimeout(() => {
									this.cloneData();
								}, 100);
								this.$bus.$emit("hideLoading");
							})
							.catch((res) => {
								console.log(res);
								this.$bus.$emit("hideLoading");
							});
						this.$set(this.masterContent, "voucher_type", 0);
						this.$set(
							this.masterContent,
							"description",
							"Nhập kho từ " + this.voucherTypeData[1]["name"].substring(3)
						);
						setTimeout(() => {
							this.cloneData();
						}, 100);
						break;
					case this.$enum.FORM_MODE.Replication:
						this.mode = this.$enum.FORM_MODE.Replication;
						this.masterContent = data["data"]["in_inward"][0];

						var dataDetail1 = data["data"]["in_inward_detail"];
						this.tableInputData = [];
						dataDetail1.forEach((item, index) => {
							this.$set(this.tableInputData, index, item["voucher_detail"]);
							this.units[index] = item["voucher_detail"]["units"];
							var mainUnit = this.tableInputData[index]["units"].find(
								(item) => {
									return item["is_main_unit"] == 1;
								}
							);
							var selectedUnitId = item["voucher_detail"]["selected_unit_id"];
							var selectedUnit = item["voucher_detail"]["units"].find(
								(item) => {
									return item["unit_id"] == selectedUnitId;
								}
							);
							this.$set(
								this.tableInputData[index],
								"main_unit_rate",
								mainUnit["rate"]
							);
							this.$set(
								this.tableInputData[index],
								"main_unit_id",
								mainUnit["unit_id"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_id",
								selectedUnit["unit_id"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_name",
								selectedUnit["unit_name"]
							);

							this.$set(
								this.tableInputData[index],
								"selected_unit_rate",
								selectedUnit["rate"]
							);

							this.$set(
								this.tableInputData[index],
								"state",
								VoucherDetailState.ADD
							);
							this.masterContent["is_mention"] = 0;
							this.enable = true;

							this.$bus.$emit("showLoading");
						});
						// Gán mã mới
						this.baseAPI
							.getNewCode()
							.then((res) => {
								console.log(res);
								this.$set(this.masterContent, "voucher_code", res.data);
								setTimeout(() => {
									this.cloneData();
								}, 100);
								this.$bus.$emit("hideLoading");
							})
							.catch((res) => {
								console.log(res);
								this.$set(this.masterContent, "voucher_code", "");
								setTimeout(() => {
									this.cloneData();
								}, 100);
								this.$bus.$emit("hideLoading");
							});

						setTimeout(() => {
							this.cloneData();
						}, 100);
						break;
				}
			});

			/**
			 * Sự kiện ẩn form
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("hideWarehouseDetail", () => {
				this.formLargeState = false;
			});

			//#region Đồng bộ dữ liệu
			/**
			 * Sự kiện thay đổi đối tượng
			 * @param {Number} index
			 * @param {String} newId
			 * @param {Array} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */

			this.$bus.$on("changeAccountObject", (index, newId, data) => {
				console.log(index, newId, data);
				var foundIdx = data.findIndex((item) => {
					return item["accountobject_id"] == newId;
				});
				this.$set(
					this.masterContent,
					"contact_address",
					data[foundIdx]["contact_address"]
				);
				this.$set(
					this.masterContent,
					"employee_id",
					data[foundIdx]["employee_id"]
				);
				this.$set(
					this.masterContent,
					"employee_name",
					data[foundIdx]["employee_name"]
				);
			});
			/**
			 * Sự kiện thay đổi hàng hoá ở hàng tiền
			 * @param {Number} index
			 * @param {String} newId
			 * @param {Array} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("changeCommodity", (index, newId, data) => {
				var foundIdx = data.findIndex((item) => {
					return item["commodity_id"] == newId;
				});
				if (foundIdx != -1) {
					this.tableInputData[index]["commodity_name"] =
						data[foundIdx]["commodity_name"];

					this.tableInputData[index]["commodity_code"] =
						data[foundIdx]["commodity_code"];

					this.tableInputData[index]["units"] = data[foundIdx]["units"];

					this.tableInputData[index]["debit_amount"] =
						data[foundIdx]["debit_amount"];

					this.$set(
						this.tableInputData[index],
						"credit_account_id",
						data[foundIdx]["credit_account_id"]
					);

					this.$set(
						this.tableInputData[index],
						"debit_account_id",
						data[foundIdx]["debit_account_id"]
					);

					this.$set(
						this.tableInputData[index],
						"warehouse_id",
						data[foundIdx]["warehouse_id"]
					);

					this.$set(
						this.tableInputData[index],
						"credit_account_number",
						data[foundIdx]["credit_account_number"]
					);

					this.$set(
						this.tableInputData[index],
						"debit_account_number",
						data[foundIdx]["debit_account_number"]
					);

					this.$set(
						this.tableInputData[index],
						"warehouse_code",
						data[foundIdx]["warehouse_code"]
					);
					// Đặt giá trị cho đơn vị tính hiện tại
					var mainUnit = this.tableInputData[index]["units"].find((item) => {
						return item["is_main_unit"] == 1;
					});
					var selectedUnit = this.tableInputData[index]["units"].find(
						(item) => {
							return item["selected"] == 1;
						}
					);
					this.$set(
						this.tableInputData[index],
						"main_unit_rate",
						mainUnit["rate"]
					);
					this.$set(
						this.tableInputData[index],
						"main_unit_id",
						mainUnit["unit_id"]
					);

					this.$set(
						this.tableInputData[index],
						"selected_unit_id",
						selectedUnit["unit_id"]
					);

					this.$set(
						this.tableInputData[index],
						"selected_unit_name",
						selectedUnit["unit_name"]
					);

					this.$set(
						this.tableInputData[index],
						"selected_unit_rate",
						selectedUnit["rate"]
					);
				}
			});
			/**
			 * Sự kiện thay đổi đơn vị tính ở hàng tiền
			 * @param {Number} index
			 * @param {String} newId
			 * @param {Array} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */

			this.$bus.$on("changeUnit", (index, newId, data) => {
				console.log(index, newId, data);
				var foundIdx = data.findIndex((item) => {
					return item["unit_id"] == newId;
				});
				if (foundIdx != -1) {
					this.tableInputData[index]["selected_unit_name"] =
						data[foundIdx]["unit_name"];
					var oldRate = this.tableInputData[index]["selected_unit_rate"];

					this.tableInputData[index]["selected_unit_rate"] =
						data[foundIdx]["rate"];
					this.tableInputData[index]["selected_unit_id"] = newId;

					if (newId == this.tableInputData[index]["main_unit_id"]) {
						this.tableInputData[index]["quantity"] =
							(this.tableInputData[index]["quantity"] * oldRate) /
							this.tableInputData[index]["main_unit_rate"];
					} else {
						this.tableInputData[index]["quantity"] =
							(this.tableInputData[index]["quantity"] /
								this.tableInputData[index]["selected_unit_rate"]) *
							oldRate;
					}
					this.tableInputData[index]["quantity"] = Math.round(
						this.tableInputData[index]["quantity"]
							? this.tableInputData[index]["quantity"]
							: 0
					);
					this.$set(this.tableInputData[index], "debit_amount", 0);
				}
			});

			/**
			 * Sự kiện thay đổi số lượng ở hàng tiền
			 * @param {Number} index
			 * @param {String} type
			 * @param {Array} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("changeQuantity", (index, type, data) => {
				if (type == "INPUT") {
					var debitAmount = Number(
						this.tableInputData[index]["debit_amount"]
							? this.tableInputData[index]["debit_amount"]
							: 0
					);
					var quantity = Number(data ? data : 0);
					this.$set(
						this.tableInputData[index],
						"total_price",
						debitAmount * quantity
					);
					this.syncTotalPrice();
				}
			});
			/**
			 * Sự kiện thay đổi đơn giá ở hàng tiền
			 * @param {Number} index
			 * @param {String} type
			 * @param {Array} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			this.$bus.$on("changeDebitAmount", (index, type, data) => {
				if (type == "INPUT") {
					var debitAmount = Number(data ? data : 0);

					var quantity = Number(
						this.tableInputData[index]["quantity"]
							? this.tableInputData[index]["quantity"]
							: 0
					);
					this.$set(
						this.tableInputData[index],
						"total_price",
						debitAmount * quantity
					);

					this.syncTotalPrice();
				}
			});
			//#endregion
		},
		methods: {
			/**
			 * Tham chiếU chứng từ
			 * CreatedBy: NTDUNG (28/09/2021)
			 */
			reference() {
				this.callDialog(
					this.$enum.DIALOG_TYPE.Warn,
					this.$resourcesVN.NOTIFY.FeatureNotAvaiable
				);
			},
			/**
			 * Tắt form
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			hideForm() {
				var data = {
					in_inward: this.masterContent,
					in_inward_detail: this.tableInputData,
				};
				// Kiểm tra đã thay đổi thì cảnh báo cất
				if (this.deepEqualObject(data, this.dataClone, ["is_mention"])) {
					if (this.needReload) this.$bus.$emit("reloadData");
					this.formLargeState = false;
				} else {
					this.callDialog(
						this.$enum.DIALOG_TYPE.ConfirmCancel,
						this.$resourcesVN.NOTIFY.DataHasChanged
					).then((answer) => {
						switch (answer) {
							case this.$enum.DIALOG_RESULT.Yes:
								this.store();
								break;
							case this.$enum.DIALOG_RESULT.No:
								this.formLargeState = false;
								if (this.needReload) this.$bus.$emit("reloadData");
								break;
							case this.$enum.DIALOG_RESULT.Cancel:
								break;
						}
					});
				}
			},
			/**
			 * Cất dữ liệu
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			store() {
				this.masterContent["voucher_type"] += "";
				var data = {
					in_inward: this.masterContent,
					in_inward_detail: this.tableInputData,
				};
				this.needReload = false;

				if (this.validateData()) {
					switch (this.mode) {
						case this.$enum.FORM_MODE.Add:
							if (!this.deepEqualObject(data, this.dataClone, ["is_mention"]))
								this.addVoucher(data);
							else this.enable = false;
							break;
						case this.$enum.FORM_MODE.Update:
							if (!this.deepEqualObject(data, this.dataClone, ["is_mention"]))
								this.updateVoucher(data);
							else this.enable = false;
							break;
						case this.$enum.FORM_MODE.Replication:
							this.addVoucher(data);
							break;
					}
				}
			},
			/**
			 * Thêm mới phiếu nhập kho
			 * @param {Object} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			addVoucher(data) {
				data["in_inward"]["created_by"] = this.$resourcesVN.ACCOUNT_NAME;
				this.$bus.$emit("showLoading");
				voucherAPI
					.addVoucher(data)
					.then((res) => {
						// Tắt loading
						this.$bus.$emit("hideLoading");
						// Thong báo thành công
						this.$bus.$emit("showToastMessage", {
							message: this.$resourcesVN.NOTIFY.AddSuccess,
							duration: 2000,
						});
						// Bind id master
						this.$set(
							this.masterContent,
							"accountvoucher_id",
							res.data.Data["newMasterId"]
						);
						// Bind id detail
						res.data.Data["newDetailIds"].forEach((item, index) => {
							this.$set(
								this.tableInputData[index],
								"accountvoucherdetail_id",
								item
							);
							this.$set(
								this.tableInputData[index],
								"state",
								VoucherDetailState.UPDATE
							);
						});
						// Bật mode cần tải lại dữ liệu
						this.needReload = true;
						// Tắt chỉnh sửa
						this.enable = false;
						// Đổi mode thành update
						this.mode = this.$enum.FORM_MODE.Update;
						// Clone lại data
						setTimeout(() => {
							this.cloneData();
						}, 100);
					})
					.catch((res) => {
						this.showError(res);
						this.$bus.$emit("hideLoading");
					});
			},
			/**
			 * Thêm mới phiếu nhập kho
			 * @param {Object} data
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			updateVoucher(data) {
				data["in_inward"]["modified_by"] = this.$resourcesVN.ACCOUNT_NAME;
				this.$bus.$emit("showLoading");
				voucherAPI
					.updateVoucher(data.in_inward.accountvoucher_id, data)
					.then((res) => {
						console.log(res);
						// Thong báo thành công
						this.$bus.$emit("showToastMessage", {
							message: this.$resourcesVN.NOTIFY.UpdateSuccess,
							duration: 2000,
						});
						// Tắt loading
						this.$bus.$emit("hideLoading");
						// Bật mode cần tải lại dữ liệu
						this.needReload = true;
						// Tắt chỉnh sửa
						this.enable = false;
						// Bind id detail
						var newData = this.tableInputData.filter((item) => {
							return item["state"] != VoucherDetailState.DELETE;
						});
						this.tableInputData = newData;

						res.data.Data["newDetailIds"].forEach((item, index) => {
							this.$set(
								this.tableInputData[index],
								"accountvoucherdetail_id",
								item
							);
							this.$set(
								this.tableInputData[index],
								"state",
								VoucherDetailState.UPDATE
							);
						});
						// Clone lại data
						setTimeout(() => {
							this.cloneData();
						}, 100);
					})
					.catch((res) => {
						this.showError(res);
						this.$bus.$emit("hideLoading");
					});
			},
			/**
			 * Cất và in
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			storeAndPrint() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Bỏ ghi
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			unMention() {
				if (this.masterContent["is_mention"]) {
					var id = [this.masterContent["accountvoucher_id"]];
					this.$bus.$emit("showLoading");
					voucherAPI
						.unMentionMany(id)
						.then((res) => {
							console.log(res);
							this.$set(this.masterContent, "is_mention", 0);
							this.$bus.$emit("reloadData");
						})
						.catch((res) => {
							console.log(res);
							this.$bus.$emit("hideLoading");
						});
				}
			},
			/**
			 * Ghi sổ
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			mention() {
				if (!this.masterContent["is_mention"]) {
					var id = [this.masterContent["accountvoucher_id"]];
					this.$bus.$emit("showLoading");
					voucherAPI
						.mentionMany(id)
						.then((res) => {
							console.log(res);
							this.$set(this.masterContent, "is_mention", 1);
							this.$bus.$emit("reloadData");
						})
						.catch((res) => {
							console.log(res);
							this.$bus.$emit("hideLoading");
						});
				}
			},
			/**
			 * Bật cho phép chỉnh sửa
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			edit() {
				console.log("edit");
				this.enable = true;
			},
			/**
			 * Clone dữ liệu hiện tại
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			cloneData() {
				this.dataClone = {
					in_inward: { ...this.masterContent },
					in_inward_detail: [],
				};
				this.tableInputData.forEach((item, index) => {
					this.dataClone.in_inward_detail[index] = { ...item };
				});
			},
			/**
			 * Validate data
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			validateData() {
				// Show Lỗi
				this.errorMsg = "";
				this.$bus.$emit("validate" + this.name);

				// Validate
				var formValid = true;
				//#region Kiểm tra required master
				var requiredMasterFields = [
					"mathematics_date",
					"voucher_date",
					"voucher_code",
				];
				requiredMasterFields.forEach((item) => {
					if (!this.masterContent[item]) formValid = false;
				});
				//#endregion
				//#region Kiểm tra required detail
				var requiredDetailFields = [
					"commodity_id",
					"commodity_name",
					"warehouse_id",
					"debit_account_id",
					"credit_account_id",
					"selected_unit_id",
					"quantity",
					"debit_amount",
				];
				this.tableInputData.forEach((row) => {
					requiredDetailFields.forEach((requiredField) => {
						if (!row[requiredField]) formValid = false;
					});
				});
				//#endregion
				//#region Kiểm tra đặc biệt
				// 1. Ngày hoạch toán phải lớn hơn hoặc bằng ngày chứng từ
				if (
					new Date(this.masterContent["mathematics_date"]) <
					new Date(this.masterContent["voucher_date"])
				)
					formValid = false;
				// 2. Phải có ít nhất một dòng trong bảng detail
				if (this.tableInputData.length == 0) {
					formValid = false;
					this.errorMsg = this.$resourcesVN.WAREHOUSE_DETAIL.RequiredVoucherDetail;
				}
				//#endregion
				return formValid;
			},
			/**
			 * Đồng bộ tổng tiền
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			syncTotalPrice() {
				var totalPrice = 0;
				this.tableInputData.forEach((item) => {
					if (item["state"] != VoucherDetailState.DELETE)
						totalPrice += Number(item["total_price"]);
				});
				this.$set(this.masterContent, "total_price", totalPrice);
			},
		},
		watch: {
			/**
			 * Khi đổi loại chứng từ thì thay đổi mô tả
			 * @param {Number} value
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			"masterContent.voucher_type": function(value) {
				this.masterContent["description"] =
					"Nhập kho từ " +
					this.voucherTypeData[Number(value)]["name"].substring(3);
			},	
			errorMsg: function(value) {
				if (value) {
					this.callDialog(this.$enum.DIALOG_TYPE.Error, this.errorMsg).then(
						() => {
							this.element.focus();
						}
					);
				}
			},
			enable: function(value) {
				if (value) {
					this.$nextTick(() => {
						this.$refs.inputFocus.$el.querySelector("input").focus();
					});
				}
			},
			formLargeState: function(value) {
				if (value && this.enable) {
					this.$nextTick(() => {
						this.$refs.inputFocus.$el.querySelector("input").focus();
					});
				}
			},
		},
		destroyed() {
			this.$bus.$off("showWarehouseDetail");
			this.$bus.$off("hideWarehouseDetail");
		},
	};
</script>
<style lang=""></style>
