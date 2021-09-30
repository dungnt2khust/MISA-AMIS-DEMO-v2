<template lang="">
	<div class="warehouseadd">
		<base-form-small
			:width="width"
			title="Thêm Đơn vị tính"
			v-if="formState"
			v-model="formState"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input label="Đơn vị tính" :required="true" />
					</div>	
					<div class=" fx-100 mt-8">
						<base-text-area label="Mô tả" />
					</div>
				</div>
			</template>
		</base-form-small>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseFormSmall from "../../../Base/Form/BaseFormSmall.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";

	export default {
		name: "WarehouseAdd",
		components: {
			BaseFormSmall,
			BaseInput,
			BaseTextArea,
		},
		props: {
			width: {
				type: String,
				default: ''
			}
		},
		data() {
			return {
				formState: false
			}
		},
		created() {
			this.$bus.$on('showWarehouseAddUnit', () => {
				this.formState = true;
			});
		}
	};
</script>
<style lang=""></style>
