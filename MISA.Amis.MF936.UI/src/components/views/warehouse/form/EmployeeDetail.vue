<template lang="">
	<div v-if="formState" class="employeedetail">
		<base-form
			@hideForm="hideForm()"
			:title="$resourcesVN.EMPLOYEE_DETAIL.Title"
		>
			<template v-slot:header>
				<div class="form__checklist fx">
					<base-checkbox
						:value="data['is_customer']"
						v-model="data['is_customer']"
						:label="$resourcesVN.EMPLOYEE_DETAIL.IsCustomer"
					/>
					<base-checkbox
						:value="data['is_supplier']"
						v-model="data['is_supplier']"
						:label="$resourcesVN.EMPLOYEE_DETAIL.IsSupplier"
					/>
				</div>
			</template>
			<template v-slot:body>
				<div class="fx-7-wrap">
					<div class="fx-7-1/2 fx-nm-wrap">
						<base-input
							:label="$resourcesVN.EMPLOYEE_DETAIL.EmployeeCode"
							:value="data['employee_code']"
							v-model="data['employee_code']"
							:required="true"
							:showRequired="true"
							:formName="name"
							ref="inputFocus"
							class="fx-2/5 mb-12"
						/>
						<base-input
							:label="$resourcesVN.EMPLOYEE_DETAIL.EmployeeName"
							:required="true"
							:value="data['employee_name']"
							v-model="data['employee_name']"
							:formName="name"
							class="fx-3/5"
						/>

						<div class="fx-1 mb-12">
							<base-combobox-advance
								:label="$resourcesVN.EMPLOYEE_DETAIL.Department"
								:controller="
									WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['controller']
								"
								v-model="
									data[WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['vmodel']]
								"
								:value="data[WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['vmodel']]"
								:vmodelField="WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['vmodel']"
								:field="WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['field']"
								:default="
									data[WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['field']]
								"
								:listGridStyle="
									WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['style']
								"
								:form="WAREHOUSE_TABLE.InWardDetail.DEPARTMENT['form']"
								type="small"
								:formName="name"
							/>
						</div>
						<base-input
							:label="$resourcesVN.EMPLOYEE_DETAIL.Position"
							:value="data['position_name']"
							v-model="data['position_name']"
							class="fx-1 mb-12"
						/>
					</div>
					<div class="fx-7-1/2 fx-nm-wrap">
						<base-input-date
							:label="$resourcesVN.EMPLOYEE_DETAIL.DateOfBirth"
							:value="data['date_of_birth']"
							v-model="data['date_of_birth']"
							class="fx-2/5 mb-12"
						/>
						<base-radio
							:label="$resourcesVN.EMPLOYEE_DETAIL.Gender"
							:options="['Nam', 'Nữ', 'Khác']"
							:value="data['gender']"
							v-model="data['gender']"
							class="fx-3/5"
						/>
						<base-input
							:label="$resourcesVN.EMPLOYEE_DETAIL.IdentityNumber"
							:value="data['identity_number']"
							v-model="data['identity_number']"
							class="fx-3/5 mb-12"
						/>
						<base-input-date
							:label="$resourcesVN.EMPLOYEE_DETAIL.IdentityDate"
							:value="data['identity_date']"
							v-model="data['identity_date']"
							class="fx-2/5"
						/>
						<base-input
							:label="$resourcesVN.EMPLOYEE_DETAIL.IdentityPlace"
							:value="data['identity_place']"
							v-model="data['identity_place']"
							class="fx-1"
						/>
					</div>
					<base-form-partition-tab
						:listPartitionTab="listPartitionTab"
						:currTab="currFormPartitionTab"
						v-model="currFormPartitionTab"
					>
						<template v-slot:body>
							<div
								v-if="currFormPartitionTab == 0"
								class="formpartitiontab__content fx-nm-wrap"
							>
								<div class="fx-7-1 fx-nm-wrap mb-12">
									<base-input
										:label="$resourcesVN.EMPLOYEE_DETAIL.Address"
										:value="data['address']"
										v-model="data['address']"
										class="fx-1"
									/>
								</div>
								<div class="fx-7-3/4 fx-nm-wrap mb-12">
									<base-input
										:label="$resourcesVN.EMPLOYEE_DETAIL.PhoneNumber"
										:value="data['phone_number']"
										v-model="data['phone_number']"
										field="PhoneNumber"
										class="fx-1/3"
									/>

									<base-input
										:label="$resourcesVN.EMPLOYEE_DETAIL.LandlineNumber"
										:value="data['landline_number']"
										v-model="data['landline_number']"
										class="fx-1/3"
									/>

									<base-input
										:label="$resourcesVN.EMPLOYEE_DETAIL.Email"
										:value="data['email']"
										v-model="data['email']"
										field="email"
										class="fx-1/3"
									/>
								</div>
							</div>
							<div
								v-if="currFormPartitionTab == 1"
								class="formpartitiontab__content"
							>
								<span class="ml-10">
									{{
										"Đang phát triển " + listPartitionTab[currFormPartitionTab]
									}}
								</span>
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
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import methods from "../../../../mixins/methods";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import baseAPI from "../../../../js/base/baseAPI";
	// COMPONENTS
	import BaseForm from "../../../Base/Form/BaseForm.vue";
	import BaseCheckbox from "../../../Base/BaseCheckbox.vue";
	import BaseInputDate from "../../../Base/BaseInputDate.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseRadio from "../../../Base/BaseRadio.vue";
	import BaseButton from "../../../Base/Button/BaseButton.vue";
	import BaseFormPartitionTab from "../../../Base/Form/components/BaseFormPartitionTab.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";

	export default {
		name: "EmployeeDetail",
		mixins: [warehouseTable, methods, globalComponents],
		components: {
			BaseForm,
			BaseCheckbox,
			BaseInputDate,
			BaseInput,
			BaseRadio,
			BaseButton,
			BaseFormPartitionTab,
			BaseComboboxAdvance,
		},
		data() {
			return {
				data: {},
				dataClone: {},
				employeeAPI: new baseAPI("Employees"),
				formState: false,
				listPartitionTab: ["Liên hệ", "Tài khoản ngân hàng"],
				currFormPartitionTab: 0,
				name: "AccountObjectGroup",
				errorMsg: "",
				element: null,
			};
		},
		created() {
			this.$bus.$on("showEmployeeDetail", () => {
				this.formState = true;

				// Bind dữ liệu
				this.bindData();
			});
			this.$bus.$on("catchError" + this.name, (msg, element) => {
				if (!this.errorMsg) {
					this.errorMsg = msg;
					this.element = element;
				}
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
				this.validateData();
				setTimeout(() => {
					if (!this.errorMsg)
						if (!this.deepEqualObject(this.data, this.dataClone)) {
							this.$bus.$emit("showLoading");
							this.employeeAPI
								.post(this.data)
								.then(() => {
									// Tắt loading
									this.$bus.$emit("hideLoading");
									// Thong báo thành công
									this.$bus.$emit("showToastMessage", {
										message: this.$resourcesVN.NOTIFY.AddSuccess,
										duration: 2000,
									});
									this.formState = false;
								})
								.catch((res) => {
									this.showError(res);
									// Tắt loading
									this.$bus.$emit("hideLoading");
								});
						} else this.formState = false;
					else
						this.callDialog(this.$enum.DIALOG_TYPE.Error, this.errorMsg).then(
							() => {
								this.element.focus();
							}
						);
				}, 100);
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
			/**
			 * Validate data
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			validateData() {
				this.errorMsg = "";
				this.$bus.$emit("validate" + this.name);
			},
			/**
			 * Bind dữ liệu
			 * CreatedBy: NTDUNG (07/10/2021)
			 */
			bindData() {
				// Lấy mã đơn vị tính
				this.employeeAPI.getNewCode().then((res) => {
					this.data = {};
					this.$set(this.data, "employee_code", res.data);
					this.$nextTick(() => {
						this.$refs.inputFocus.$el.querySelector("input").focus();
					});
					this.cloneData();
				});
			},
		},
	};
</script>
<style lang=""></style>
