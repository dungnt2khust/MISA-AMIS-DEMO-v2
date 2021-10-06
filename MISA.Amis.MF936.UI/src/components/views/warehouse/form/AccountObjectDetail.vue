<template lang="">
	<div v-if="formState" class="accountvoucherdetail">
		<base-form
			@hideForm="hideForm()"
			:title="$resourcesVN.ACCOUNT_OBJECT_DETAIL.Title"
		>
			<template v-slot:header>
				<div class="form__checklist fx">
					<base-radio
						:options="[
							$resourcesVN.ACCOUNT_OBJECT_DETAIL.Organization,
							$resourcesVN.ACCOUNT_OBJECT_DETAIL.Personal,
						]"
						:value="data['account_object_type']"
						v-model="data['account_object_type']"
					/>
					<base-checkbox
						:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.IsSupplier"
						:value="data['is_supplier']"
						v-model="data['is_supplier']"
					/>
				</div>
			</template>
			<template v-slot:body>
				<div class="fx-7-wrap">
					<div class="fx-7-1/2 fx-nm-wrap">
						<base-input
							:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.AccountObjectCode"
							ref="InputCode"
							:required="true"
							:value="data['account_object_code']"
							v-model="data['account_object_code']"
							class="fx-3/5 mb-12"
						/>
						<base-input
							:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.TaxCode"
							:value="data['tax_code']"
							v-model="data['tax_code']"
							class="fx-2/5"
						/>

						<div class="fx-1/3">
							<base-combobox-advance
								:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.SupplierName"
								type="small"
								:placeholder="$resourcesVN.ACCOUNT_OBJECT_DETAIL.Vocative"
								:api="WAREHOUSE_TABLE.InWardDetail.VOCATIVE['api']"
								:controller="
									WAREHOUSE_TABLE.InWardDetail.VOCATIVE['controller']
								"
								v-model="
									data[
										WAREHOUSE_TABLE.InWardDetail.VOCATIVE['vmodel']
									]
								"
								:value="
									data[
										WAREHOUSE_TABLE.InWardDetail.VOCATIVE['vmodel']
									]
								"
								:vmodelField="WAREHOUSE_TABLE.InWardDetail.VOCATIVE['vmodel']"
								:field="WAREHOUSE_TABLE.InWardDetail.VOCATIVE['field']"
								:default="
									data[
										WAREHOUSE_TABLE.InWardDetail.VOCATIVE['field']
									]
								"
								:listGridStyle="
									WAREHOUSE_TABLE.InWardDetail.VOCATIVE['style']
								"
								:form="WAREHOUSE_TABLE.InWardDetail.VOCATIVE['form']"
							/>
						</div>
						<base-input
							label="."
							:placeholder="$resourcesVN.ACCOUNT_OBJECT_DETAIL.FullName"
							class="fx-2/3 mb-12"
							:value="data['account_object_name']"
							v-model="data['account_object_name']"
						/>
						<base-text-area
							:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.Address"
							:placeholder="$resourcesVN.ACCOUNT_OBJECT_DETAIL.AddressExample"
							class="fx-1"
							:value="data['contact_address']"
							v-model="data['contact_address']"
						/>
						<div class="fx-1 mb-12"></div>
					</div>
					<div class="fx-7-1/2 fx-nm-wrap">
						<div class="fx-1 mb-12">
							<base-combobox-advance
								:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.AccountObjectGroup"
								:api="WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['api']"
								:controller="
									WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['controller']
								"
								v-model="
									data[
										WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['vmodel']
									]
								"
								:value="
									data[
										WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['vmodel']
									]
								"
								:vmodelField="WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['vmodel']"
								:field="WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['field']"
								:default="
									data[
										WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['field']
									]
								"
								:listGridStyle="
									WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['style']
								"
								:form="WAREHOUSE_TABLE.InWardDetail.ACCOUNT_OBJECT_GROUP['form']"
							/>
						</div>
						<div class="fx-1">
							<base-combobox-advance
								:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.Employee"
								:api="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['api']"
								:controller="
									WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['controller']
								"
								v-model="
									data[
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']
									]
								"
								:value="
									data[
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']
									]
								"
								:vmodelField="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['vmodel']"
								:field="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['field']"
								:default="
									data[
										WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['field']
									]
								"
								:listGridStyle="
									WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['style']
								"
								:form="WAREHOUSE_TABLE.InWardDetail.EMPLOYEE['form']"
							/>
						</div>
						<div class="fx-1"></div>
					</div>
					<base-form-partition-tab
						:listPartitionTab="listPartitionTab"
						:currTab="currFormPartitionTab"
						v-model="currFormPartitionTab"
					>
						<template v-slot:body>
							<div
								v-if="currFormPartitionTab == 0"
								class="formpartitiontab__content fx-7-wrap"
							>
								<div class="fx-7-1/2 fx-nm-wrap">
									<base-input
										:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.ContactInfo"
										:placeholder="$resourcesVN.ACCOUNT_OBJECT_DETAIL.Email"
										:value="data['email']"
										v-model="data['email']"
										class="fx-1 mb-6"
									/>
									<base-input
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.PhoneNumber
										"
										:value="data['phone_number']"
										v-model="data['phone_number']"
										class="fx-1/2 mr-10 mb-6"
										/>
									<base-input
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.LandlineNumber
										"
										:value="data['landline_number']"
										v-model="data['landline_number']"
										class="fx-1/2 mb-6"
									/>
									<base-input
										:label="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.LegalRepresentative
										"
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.LegalRepresentative
										"
										:value="data['legal_representation']"
										v-model="data['legal_representation']"										
										class="fx-1 mb-6"
									/>
								</div>
								<div class="fx-7-1/2 fx-nm-wrap">
									<base-input
										:label="$resourcesVN.ACCOUNT_OBJECT_DETAIL.ContactInfo"
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.IdentityNumber
										"
										:value="data['identity_number']"
										v-model="data['identity_number']"
										class="fx-1/2 mb-6"
										/>
									<base-input-date
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.IdentityDate
										"
										:value="data['identity_date']"
										v-model="data['identity_date']"
										class="fx-1/2 mb-6 mr-6"
										/>
									<base-input
										:placeholder="
											$resourcesVN.ACCOUNT_OBJECT_DETAIL.IdentityPlace
										"
										:value="data['identity_place']"
										v-model="data['identity_place']"
										class="fx-1 mb-6"
									/>
								</div>
							</div>
							<div
								v-if="currFormPartitionTab == 1"
								class="formpartitiontab__content"
							>
								<span class="ml-10">
									{{
										$resourcesVN.ACCOUNT_OBJECT_DETAIL.FeatureNotAvaiable + ' ' + listPartitionTab[currFormPartitionTab]
									}}</span
								>
							</div>
							<div
								v-if="currFormPartitionTab == 2"
								class="formpartitiontab__content"
							>
								<span class="ml-10">
									{{
										$resourcesVN.ACCOUNT_OBJECT_DETAIL.FeatureNotAvaiable + ' ' + listPartitionTab[currFormPartitionTab]
									}}</span
								>
							</div>
						</template>
					</base-form-partition-tab>
				</div>
			</template>
			<template v-slot:footer>
				<base-button
					:method="hideForm"
					:label="$resourcesVN.FORM.Close.Label"
				/>
				<div class="form__control">
					<base-button :method="store" :label="$resourcesVN.FORM.Store.Label" />
					<base-button
						type="green"
						:method="storeAndAdd"
						:label="$resourcesVN.FORM.StoreAndPrint.Label"
					/>
				</div>
			</template>
		</base-form>
	</div>
</template>
<script>
	// LIBRARY
	import methods from "../../../../mixins/methods";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import baseAPI from "../../../../js/base/baseAPI";
	// COMPONENTS
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import BaseForm from "../../../Base/Form/BaseForm.vue";
	import BaseCheckbox from "../../../Base/BaseCheckbox.vue";
	import BaseInputDate from "../../../Base/BaseInputDate.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseRadio from "../../../Base/BaseRadio.vue";
	import BaseButton from "../../../Base/Button/BaseButton.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";
	import BaseFormPartitionTab from "../../../Base/Form/components/BaseFormPartitionTab.vue";

	export default {
		name: "AccoutObjectDetail",
		mixins: [warehouseTable, methods, globalComponents],
		components: {
			BaseForm,
			BaseCheckbox,
			BaseInputDate,
			BaseInput,
			BaseButton,
			BaseRadio,
			BaseComboboxAdvance,
			BaseTextArea,
			BaseFormPartitionTab,
		},
		data() {
			return {
				currFormPartitionTab: 0,
				listPartitionTab: [
					"Liên hệ",
					"Điều khoản thanh toán",
					"Tài khoản ngân hàng",
				],
				formState: false,
				data: {},
				dataClone: {},
				accountObjectAPI: new baseAPI("AccountObjects"),
			};
		},
		created() {
			this.$bus.$on("showAccountObjectDetail", () => {
				this.data = {};
				this.formState = true;
				setTimeout(() => {this.cloneData()}, 100);
			});
		},
		methods: {
			/**
			 * Tắt form
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			hideForm() {
				if (this.deepEqualObject(this.data, this.dataClone))
					this.formState = false;
				else
					this.callDialog(
						this.$enum.DIALOG_TYPE.ConfirmCancel,
						this.$resourcesVN.NOTIFY.DataHasChanged
					).then((answer) => {
						switch (answer) {
							case this.$enum.DIALOG_RESULT.Yes:
								this.store();
								break;
							case this.$enum.DIALOG_RESULT.No:
								this.formState = false;
								break;
							case this.$enum.DIALOG_RESULT.Cancel:
								break;
						}
					});
			},
			/**
			 * Cất
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			store() {
				if (!this.deepEqualObject(this.data, this.dataClone)) {
					this.accountObjectAPI
						.post(this.data)
						.then((res) => {
							console.log(res);
						})
						.catch((res) => {
							console.log(res);
						});
					this.formState = false;
				} else this.formState = false;
			},
			/**
			 * Cất và thêm
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			storeAndAdd() {
				console.log(this.data);
			},
			/**
			 * Clone data
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			cloneData() {
				this.dataClone = { ...this.data };
			},
		},	
	};
</script>
<style lang=""></style>
