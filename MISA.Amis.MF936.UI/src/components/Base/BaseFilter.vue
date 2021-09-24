<template lang="">
	<div class="filter">
		<div
			@click="filterPopupState = !filterPopupState"
			class="filter__btn button"
		>
			Lọc
		</div>
		<div v-show="filterPopupState" class="fx-wrap filter__popup">
			<div class="fx-100 filter__popup-item">
				<base-combobox
					label="Trạng thái ghi sổ"
					:comboboxData="filterData['mention']"
					:currIdx="currMention"
					v-model="currMention"
				/>
			</div>
			<div class="fx-100 filter__popup-item">
				<base-combobox
					label="Loại nhập"
					:comboboxData="filterData['type']"
					:currIdx="currType"
					v-model="currType"
				/>
			</div>
			<div class="fx-1/2 filter__popup-item">
				<base-combobox
					label="Kỳ báo cáo"
					:comboboxData="filterData['period']"
					:currIdx="currPeriod"
					v-model="currPeriod"
				/>
			</div>
			<div class="fx-1/4 filter__popup-item">
				<base-input-date
					:notmap="true"
					label="Từ ngày"
					:value="currFromDate"
					v-model="currFromDate"
				/>
			</div>
			<div class="fx-1/4 filter__popup-item">
				<base-input-date
					:notmap="true"
					label="Đến ngày"
                    :value="currToDate"
					v-model="currToDate"
				/>
			</div>
			<div class="fx-100 filter__popup-item text-blue">
				Lọc nâng cao
			</div>
			<div class="fx-100 fx-space-between filter__popup-btns">
				<div @click="resetFilter()" class="button">Đặt lại</div>
				<div @click="filter()" class="button button--green">Lọc</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import BaseInputDate from "./BaseInputDate.vue";
	import BaseCombobox from "./Select/BaseCombobox.vue";

	export default {
		name: "BaseFilter",
		components: {
			BaseInputDate,
			BaseCombobox,
		},
		data() {
			return {
				filterPopupState: false,
				currMention: -1,
				currType: -1,
				currPeriod: -1,
				currFromDate: null,
				currToDate: null,
				filterData: {
					mention: [
						{
							name: "Tất cả",
						},
						{ name: "Đã ghi sổ" },
						{ name: "Chưa ghi sổ" },
					],
					type: [
						{ name: "Thành phần sản xuất" },
						{ name: "Hàng bán bị trả lại" },
						{ name: "Khác" },
					],
					period: [
						{ name: "Đầu năm đến hiện tại" },
						{ name: "Sáu tháng đầu năm" },
						{ name: "Sáu tháng cuối năm" },
						{ name: "Tháng 1" },
						{ name: "Tháng 2" },
					],
				},
			};
		},
        methods: {
            resetFilter() {
                this.currMention = -1;
                this.currType = -1;
                this.currPeriod = -1;
                this.currFromDate = null;
                this.currToDate = null;
            },
            filter() {
                this.filterPopupState = false;
                console.log(this.currMention, this.currType, this.currPeriod, this.currFromDate, this.currToDate);
            }
        }
	};
</script>
<style>
	@import url("../../css/base/filter.css");
</style>
