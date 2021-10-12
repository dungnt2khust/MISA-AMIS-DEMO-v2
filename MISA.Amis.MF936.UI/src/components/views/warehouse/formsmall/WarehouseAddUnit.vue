<template lang="">
	<div v-if="formState" class="warehouseadd">
		<base-form-small
			:width="width"
			title="Thêm Đơn vị tính"
			@hideForm="hideForm()"
			@store="store()"
			@storeAndAdd="storeAndAdd()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Đơn vị tính"
							:value="data['unit_name']"
							v-model="data['unit_name']"
							:required="true"
							:showRequired="true"
							:formName="name"
							ref="inputFocus"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Mã đơn vị tính"
							:value="data['unit_code']"
							v-model="data['unit_code']"
							:required="true"
							:showRequired="true"
							:formName="name"
						/>
					</div>
					<div class=" fx-1 mt-8">
						<base-text-area
							:value="data['description']"
							v-model="data['description']"
							label="Mô tả"
						/>
					</div>
				</div>
			</template>
			<template v-slot:footer>
				<base-button :method="hideForm" label="Huỷ" />
				<div class="fx">
					<base-button :method="store" label="Cất" class="mr-10" />
					<base-button :method="storeAndAdd" type="green" label="Cất và Thêm" />
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
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";

	export default {
		name: "WarehouseAdd",
		mixins: [methods, globalComponents],
		components: {
			BaseFormSmall,
			BaseInput,
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
				unitAPI: new baseAPI("Units"),
				name: "Unit",
				errorMsg: "",
				element: null,
			};
		},
		created() {
			this.$bus.$on("showWarehouseAddUnit", () => {
				this.formState = true;

				// Bind dữ liệu
				this.bindData();

				this.$bus.$on("catchError" + this.name, (msg, element) => {
					if (!this.errorMsg) {
						this.errorMsg = msg;
						this.element = element;
					}
				});
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
			store(mode) {
				this.validateData();
				setTimeout(() => {
					if (!this.errorMsg)
						if (!this.deepEqualObject(this.data, this.dataClone)) {
							this.$bus.$emit("showLoading");
							this.unitAPI
								.post(this.data)
								.then(() => {
									// Tắt loading
									this.$bus.$emit("hideLoading");
									// Thong báo thành công
									this.$bus.$emit("showToastMessage", {
										message: this.$resourcesVN.NOTIFY.AddSuccess,
										duration: 2000,
									});
									if (!mode)
										this.formState = false;
									else
										this.bindData();
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
				this.store(1);
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
				this.unitAPI.getNewCode().then((res) => {
					this.data = {};
					this.$set(this.data, "unit_code", res.data);
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
