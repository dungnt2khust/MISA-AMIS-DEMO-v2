<template lang="">
	<div class="warehouseaddcommoditygroup">
		<base-form-small
			:width="width"
			title="Thêm nhóm vật tư, hàng hoá, dịch vụ"
		>
			<template v-slot:body>
				<div class="fx-wrap">
					<div class="mt-8 fx-1/3">
						<base-input label="Mã" :required="true" />
					</div>
					<div class="mt-8 fx-2/3">
						<base-input label="Tên" :required="true" />
					</div>
					<div class="fx-100 mt-8">
						<base-combobox-advance label="Tài khoản kho" />
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
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";

	export default {
		name: "WarehouseAddCommodityGroup",
		components: {
			BaseFormSmall,
			BaseInput,
			BaseComboboxAdvance,
		},
		props: {
			width: {
				type: String,
				default: ''
			}
		}
	};
</script>
<style lang=""></style>
