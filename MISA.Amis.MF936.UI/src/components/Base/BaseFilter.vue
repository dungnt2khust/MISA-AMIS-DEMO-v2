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
                <base-input label="Trạng thái ghi sổ"/>
            </div>
            <div class="fx-100 filter__popup-item">
                <base-input label="Loại nhập"/>
            </div>
            <div class="fx-1/2 filter__popup-item">
                <base-input label="Kỳ báo cáo"/>
            </div>
            <div class="fx-1/4 filter__popup-item">
                <base-input-date label="Từ ngày"/>
            </div>
            <div class="fx-1/4 filter__popup-item">
                <base-input-date label="Đến ngày"/>
            </div>
            <div class="fx-100 filter__popup-item text-blue">
                Lọc nâng cao
            </div>
            <div class="fx-100 fx-space-between filter__popup-btns">
                <div class="button"> Đặt lại</div>
                <div class="button button--green"> Lọc </div>
            </div>
        </div>
	</div>
</template>
<script>
    // LIBRARY 
    import BaseInput from './BaseInput.vue'
    import BaseInputDate from './BaseInputDate.vue'

	export default {
		name: "BaseFilter",
        components: {
            BaseInput,
            BaseInputDate
        },
		data() {
			return {
				filterPopupState: false
			};
		},
	};
</script>
<style>
	@import url("../../css/base/filter.css");
</style>
