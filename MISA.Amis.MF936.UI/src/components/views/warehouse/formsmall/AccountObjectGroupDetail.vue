<template lang="">
	<div v-if="formState" class="accountobjectgroupdetail">
		<base-form-small
			width="600px"
			title="Thêm nhóm khách hàng, nhà cung cấp"
			@hideForm="hideForm()"
			@store="store()"
			@storeAndAdd="storeAndAdd()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Mã"
							:value="data['account_object_group_code']"
							v-model="data['account_object_group_code']"
							:required="true"
							:showRequired="true"
							:formName="name"
							ref="inputFocus"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Tên"
							:value="data['account_object_group_name']"
							v-model="data['account_object_group_name']"
							:required="true"
							:showRequired="true"
							:formName="name"
						/>
					</div>
					<div class=" fx-1 mt-8">
						<base-text-area
							label="Mô tả"
							:value="data['description']"
							v-model="data['description']"
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
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";

	export default {
		name: "AccountObjectGroupDetail",
		mixins: [globalComponents, methods],
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
				accountObjectGroupAPI: new baseAPI("AccountObjectGroups"),
				name: "AccountObjectGroup",
				errorMsg: "",
				element: null,
			};
		},
		created() {
			this.$bus.$on("showAccountObjectGroupDetail", () => {
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
							this.accountObjectGroupAPI
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
				this.accountObjectGroupAPI.getNewCode().then((res) => {
					this.data = {};
					this.$set(this.data, "account_object_group_code", res.data);
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
