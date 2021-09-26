<template lang="">
	<div>
		<base-form-large v-model="formLargeState" :formLargeState="formLargeState">
			<template v-slot:header>
				<div class="formlarge__type ml-40">
					<base-combobox-old class="mr-16" />
					<base-combobox-advance
						width="300px"
						type="search"
						placeholder="Nhập số phiếu xuất từ chi nhánh khác chuyển đến"
					/>
				</div>
			</template>

			<template v-slot:body>
				<div class="formlarge__body-part1 fx-wrap">
					<div class="formlarge__body-main-info w-3/4">
						<div class="w-4/5 fx-wrap pr-10 border-right">
							<div class="fx-3/7 mb-10">
								<base-combobox-advance :tabindex="0" label="Khách hàng" />
							</div>
							<div class="fx-4/7 mb-10">
								<base-input :tabindex="1" label="Địa chỉ" />
							</div>
							<div class="fx-3/7 mb-10">
								<base-input :tabindex="2" label="Người giao hàng" />
							</div>
							<div class="fx-4/7 mb-10">
								<base-input :tabindex="3" label="Diễn giải" />
							</div>
							<div class="fx-item mb-10">
								<base-input
									label="Kèm theo"
									unit="chứng từ gốc"
									width="120px"
									:tabindex="4"
								/>
							</div>
						</div>
						<div class="w-1/5 ml-16">
							<div class="fx-100 mb-10">
								<base-input-date :tabindex="5" label="Ngày hạch toán" />
							</div>
							<div class="fx-100 mb-10">
								<base-input-date :tabindex="6" label="Ngày chứng từ" />
							</div>
							<div class="fx-100 mb-10">
								<base-input :tabindex="7" label="Số chứng từ" />
							</div>
						</div>
					</div>
					<div class="formlarge__body-cash w-1/4">
						<div class="formlarge__body-cash-title">Tổng tiền</div>
						<h1 class="formlarge__body-cash-number">0,0</h1>
					</div>
					<div class="formlarge__reference w-2/5 mb-20 mt-20">
						Tham chiếu
						<div class="formlarge__reference-icon"></div>
					</div>
				</div>
				<!-- Escap -->
				<div class="formlarge__body-part2">
					<base-table-input v-model="selectCommodityState" />
				</div>
			</template>
		</base-form-large>
		<warehouse-select-commodity
			v-if="selectCommodityState"
			v-model="selectCommodityState"
		/>	
		<warehouse-add width="500px" v-if="false"/>
		<warehouse-add-commodity-group width="600px" v-if="false"/>
		<warehouse-add-unit width="500px" v-if="false"/>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseFormLarge from "../../../Base/Form/BaseFormLarge.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseInputDate from "../../../Base/BaseInputDate.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseComboboxOld from "../../../Base/Old/BaseComboboxOld.vue";
	import BaseTableInput from "../../../Base/Table/BaseTableInput.vue";
	import WarehouseSelectCommodity from "../formextend/WarehouseSelectCommodity.vue";
	import WarehouseAdd from "../formsmall/WarehouseAdd.vue"
	import WarehouseAddCommodityGroup from "../formsmall/WarehouseAddCommodityGroup.vue"
	import WarehouseAddUnit from "../formsmall/WarehouseAddUnit.vue"

	export default {
		name: "WarehouseDetail",
		components: {
			BaseFormLarge,
			BaseInput,
			BaseInputDate,
			BaseComboboxAdvance,
			BaseComboboxOld,
			BaseTableInput,
			WarehouseSelectCommodity,
			WarehouseAdd,
			WarehouseAddCommodityGroup,
			WarehouseAddUnit
		},
		data() {
			return {
				formLargeState: false,
				selectCommodityState: false,
				createCommodityState: false,
			};
		},
		created() {
			this.$bus.$on("showWarehouseDetail", () => {
				this.formLargeState = true;
			});
			this.$bus.$on("hideWarehouseDetail", () => {
				this.formLargeState = false;
			});
		},	
		destroyed() {
			this.$bus.$off("showWarehouseDetail");
			this.$bus.$off("hideWarehouseDetail");
		},
	};
</script>
<style lang=""></style>
