<template lang="">
	<div v-if="formState" class="warehouseaddcommoditygroup">
		<base-form-small
			:width="width"
			title="Thêm nhóm vật tư, hàng hoá, dịch vụ"
			@hideForm="hideForm()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Mã"
							:value="data['commodity_group_code']"
							v-model="data['commodity_group_code']"
							:required="true"
							:formName="name"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Tên"
							:value="data['commodity_group_name']"
							v-model="data['commodity_group_name']"
							:required="true"
							:formName="name"
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
	import BaseButton from "../../../Base/Button/BaseButton.vue";

	export default {
		name: "WarehouseAddCommodityGroup",
		mixins: [globalComponents, methods],
		components: {
			BaseFormSmall,
			BaseInput,
			BaseButton,
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
			};
		},
		created() {
			this.$bus.$on("showWarehouseAddCommodityGroup", () => {
				this.formState = true;
				this.cloneData();
			});
			this.$bus.$on("catchError" + this.name, (data) => {
				if (!this.errorMsg) {
					this.errorMsg = data;
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
				this.errorMsg = "";
				this.$bus.$emit("validate" + this.name);
				setTimeout(() => {
					if (!this.errorMsg)
						if (!this.deepEqualObject(this.data, this.dataClone)) {
							this.commodityGroupAPI
								.post(this.data)
								.then((res) => {
									console.log(res);
								})
								.catch((res) => {
									console.log(res);
								});
							this.formState = false;
						} else this.formState = false;
					else {
						this.callDialog(this.$enum.DIALOG_TYPE.Error, 'Sai mẹ thông tin rồi, nhập lại đê');
					}
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
		},
	};
</script>
<style lang=""></style>
