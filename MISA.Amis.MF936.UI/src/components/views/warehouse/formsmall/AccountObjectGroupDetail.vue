<template lang="">
	<div v-if="formState" class="accountobjectgroupdetail">
		<base-form-small
			width="600px"
			title="Thêm nhóm khách hàng, nhà cung cấp"
			@hideForm="hideForm()"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input
							label="Mã"
							:value="data['account_object_group_code']"
							v-model="data['account_object_group_code']"
							:required="true"
						/>
					</div>
					<div class="mt-8 fx-2/3">
						<base-input
							label="Tên"
							:value="data['account_object_group_name']"
							v-model="data['account_object_group_name']"
							:required="true"
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
	import BaseButton from "../../../Base/Button/BaseButton.vue";

	export default {
		name: "AccountObjectGroupDetail",
		mixins: [globalComponents, methods],
		components: {
			BaseFormSmall,
			BaseInput,
			BaseTextArea,
			BaseButton
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
				accountObjectGroupAPI: new baseAPI("AccountObjectGroups"),
			};
		},
		created() {
			this.$bus.$on("showAccountObjectGroupDetail", () => {
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
					this.accountObjectGroupAPI
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
