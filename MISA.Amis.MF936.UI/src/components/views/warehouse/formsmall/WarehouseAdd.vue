<template lang="">
	<div v-if="formState" class="warehouseadd">
		<base-form-small
			:width="width"
			title="Thêm Kho"
			@hideForm="hideForm()"
			@store="store()"
			@storeAndAdd="storeAndAdd()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Mã"
							:required="true"
							:value="data['warehouse_code']"
							v-model="data['warehouse_code']"
							:formName="name"
							:showRequired="true"
							ref="inputFocus"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Tên"
							ref="firstFocus"
							:required="true"
							:value="data['warehouse_name']"
							v-model="data['warehouse_name']"
							:formName="name"
							:showRequired="true"
						/>
					</div>
					<div class="fx-1/3 mt-8">
						<base-combobox-advance
							label="Tài khoản kho"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['controller']
							"
							v-model="
								data[WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']]
							"
							:value="
								data[WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']]
							"
							:vmodelField="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']
							"
							:field="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['field']"
							:default="
								data[WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['field']]
							"
							:subfield="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['subfield']
							"
							:display="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['display']
							"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['form']"
							:formName="name"
						/>
					</div>
					<div class=" fx-1 mt-8">
						<base-text-area
							label="Địa chỉ"
							:value="data['warehouse_address']"
							v-model="data['warehouse_address']"
						/>
					</div>
				</div>
			</template>
		</base-form-small>
	</div>
</template>
<script>
	// LIBRARY
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	import methods from "../../../../mixins/methods";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import baseAPI from "../../../../js/base/baseAPI";
	// COMPONENTS
	import BaseFormSmall from "../../../Base/Form/BaseFormSmall.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";

	export default {
		name: "WarehouseAdd",
		mixins: [warehouseTable, methods, globalComponents],
		components: {
			BaseFormSmall,
			BaseInput,
			BaseComboboxAdvance,
			BaseTextArea,
		},
		props: {
			width: {
				type: String,
				default: "",
			},
		},
		data() {
			return {
				formState: false,
				data: {},
				dataClone: {},
				warehouseAPI: new baseAPI("Warehouses"),
				name: "Warehouse",
				errorMsg: "",
				element: null,
			};
		},
		created() {
			this.$bus.$on("showWarehouseAdd", () => {
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
							this.warehouseAPI
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
				// Lấy mã kho mới
				this.warehouseAPI.getNewCode().then((res) => {
					this.data = {};
					this.$set(this.data, "warehouse_code", res.data);
					this.$refs.inputFocus.$el.querySelector("input").focus();
					this.cloneData();
				});
			},
		},
	};
</script>
<style lang=""></style>
