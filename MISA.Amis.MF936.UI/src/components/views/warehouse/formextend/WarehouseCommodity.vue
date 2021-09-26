<template lang="">
	<div class="commodity">
		<div class="fx-wrap mt-20">
			<base-input label="Tên" :required="true" class="fx-100" />
			<base-input label="Mã" :required="true" class="fx-1/5 mt-8" />
            <base-combobox-multi-choose label="Nhóm VTHH" class="fx-4/5 mt-8" />
			<base-combobox-advance
				label="Đơn vị tính chính"
				class="fx-100 mt-8"
				width="200px"
			/>
			<base-text-area label="Mô tả" class="fx-100 mt-8"/>
		</div>
		<base-form-partition label="Thông tin ngầm định">
			<template v-slot:body>
				<div class="mt-20">
					<div class="fx">
						<base-combobox-advance
							label="Kho ngầm định"
							class="pr-26"
							width="150px"
						/>
						<base-combobox-advance
							label="TK giảm giá"
							class="pr-26"
							type="small"
							width="150px"
						/>
						<base-input
							label="Đơn giá mua gần nhất"
							width="155px"
						/>
					</div>
					<div class="fx">
						<base-combobox-advance
							label="Kho ngầm định"
							class="pr-26 mt-16"
							type="small"
							width="150px"
						/>
						<base-combobox-advance
							label="TK giảm giá"
							class="pr-26 mt-16"
							width="150px"
							type="small"
						/>
					</div>	
				</div>
			</template>
		</base-form-partition>

		<base-form-partition label="Đơn vị chuyển đổi">
			<template v-slot:body>
				<base-form-info
                    class="mt-8"
					content="
                        Trường hợp vật tư, hàng hóa sử dụng đơn vị tính khác nhau khi nhập/xuất thì khai báo thêm các đơn vị tính ở đây.
                        VD: 1 tạ = 100kg, 1 két bia = 20 chai , ..."
				/>
				<base-table-input class="mt-20"/>
			</template>
		</base-form-partition>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseFormPartition from "../../../Base/Form/components/BaseFormPartition.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseComboboxMultiChoose from "../../../Base/Select/BaseComboboxMultiChoose.vue";
	import BaseFormInfo from "../../../Base/Form/components/BaseFormInfo.vue";
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue"
	import BaseTableInput from "../../../Base/Table/BaseTableInput.vue"

	export default {
		name: "WarehouseCommodity",
		components: {
			BaseFormPartition,
			BaseInput,
			BaseComboboxAdvance,
			BaseComboboxMultiChoose,
			BaseFormInfo,
			BaseTextArea,
			BaseTableInput
		},
		methods: {
			hideForm() {
				this.$emit("input", false);
			},
		},
	};
</script>
<style></style>
