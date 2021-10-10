<template lang="">
	<div v-if="formState" class="warehouseaddcommoditygroup">
		<base-form-small
			:width="width"
			title="Thêm nhóm vật tư, hàng hoá, dịch vụ"
			@hideForm="hideForm()"
			@store="store()"
			@storeAndAdd="storeAndAdd()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Mã"
							:value="data['commodity_group_code']"
							v-model="data['commodity_group_code']"
							:required="true"
							:showRequired="true"
							:formName="name"
							ref="inputFocus"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Tên"
							:value="data['commodity_group_name']"
							v-model="data['commodity_group_name']"
							:required="true"
							:showRequired="true"
							:formName="name"
						/>
					</div>
				</div>
			</template>
		</base-form-small>
	</div>
</template>
<script>
	// LIBRARY
	import methods from "../../../../mixins/methods";
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js";
	import baseAPI from "../../../../js/base/baseAPI";
	// COMPONENTS
	import BaseFormSmall from "../../../Base/Form/BaseFormSmall.vue";
	import BaseInput from "../../../Base/BaseInput.vue";

	export default {
		name: "WarehouseAddCommodityGroup",
		mixins: [globalComponents, methods],
		components: {
			BaseFormSmall,
			BaseInput,
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
				commodityGroupAPI: new baseAPI("CommodityGroups"),
				name: "AddCommodityGroup",
				errorMsg: "",
				element: null,
			};
		},
		created() {
			this.$bus.$on("showWarehouseAddCommodityGroup", () => {
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
							this.commodityGroupAPI
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
				this.data = {};
				this.$nextTick(() => {
					this.$refs.inputFocus.$el.querySelector("input").focus();
				});
				this.cloneData();
			},
		},
	};
</script>
<style lang=""></style>
