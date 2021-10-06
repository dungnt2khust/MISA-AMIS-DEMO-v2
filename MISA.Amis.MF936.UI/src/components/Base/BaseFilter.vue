<template lang="">
	<div class="filter">
		<div @click="toggleFilter($event)" class="filter__btn button">
			Lọc
		</div>
		<div
			ref="filterPopup"
			v-show="filterPopupState"
			:style="{ top: top + 'px', left: left + 'px' }"
			class="fx-wrap filter__popup"
		>
			<div class="fx-1 filter__popup-item">
				<base-combobox
					label="Trạng thái ghi sổ"
					:comboboxData="filterData['mention']"
					:currIdx="currMention"
					v-model="currMention"
				/>
			</div>
			<div class="fx-1 filter__popup-item">
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
			<div class="fx-1 filter__popup-item text-blue">
				Lọc nâng cao
			</div>
			<div class="fx-1 fx-space-between filter__popup-btns">
				<div @click="resetFilter()" class="button">Đặt lại</div>
				<div @click="filter()" class="button button--green">Lọc</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import date from "../../mixins/date/date.js";
	import methods from "../../mixins/methods.js";
	// COMPONENTS
	import BaseInputDate from "./BaseInputDate.vue";
	import BaseCombobox from "./Select/BaseCombobox.vue";

	export default {
		name: "BaseFilter",
		mixins: [date, methods],
		components: {
			BaseInputDate,
			BaseCombobox,
		},
		data() {
			return {
				top: 0,
				left: 0,
				filterPopupState: false,
				currMention: 0,
				currType: 0,
				currPeriod: 0,
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
						{ name: "Tất cả"},
						{ name: "Thành phần sản xuất" },
						{ name: "Hàng bán bị trả lại" },
						{ name: "Khác" },
					],
					period: [
						{ name: "Năm nay", key: "year"},
						{ name: "Hôm nay", key: "today" },
						{ name: "Đầu năm đến hiện tại", key: "beginyeartocurr" },
						{ name: "Sáu tháng đầu năm", key: "firsthalfyear" },
						{ name: "Sáu tháng cuối năm", key: "secondhalfyear" },
						{ name: "Tháng 1", key: "jan" },
						{ name: "Tháng 2", key: "feb" },
						{ name: "Tháng 3", key: "mar" },
						{ name: "Tháng 4", key: "apr" },
						{ name: "Tháng 5", key: "may" },
						{ name: "Tháng 6", key: "jun" },
						{ name: "Tháng 7", key: "jul" },
						{ name: "Tháng 8", key: "aug" },
						{ name: "Tháng 9", key: "sep" },
						{ name: "Tháng 10", key: "oct" },
						{ name: "Tháng 11", key: "nov" },
						{ name: "Tháng 12", key: "dec" },
					],
				},
			};
		},
		mounted() {
			this.bindDate(this.currPeriod);
			this.filter();
		},
		methods: {
			toggleFilter(event) {
				this.filterPopupState = !this.filterPopupState;

				var filterButton = this.findParentByClass(event.target, "filter__btn");
				// var filterPopup = this.$refs.filterPopup;
				this.$nextTick(() => {
					var position = this.positionPopup(
						filterButton.getBoundingClientRect(),
						// filterPopup.getBoundingClientRect()
					);
					this.top = position.top;
					this.left = position.left;
				});
			},
			/**
			 * Đặt lại giá trị mặc định
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			resetFilter() {
				this.currMention = 0;
				this.currType = 0;
				this.currPeriod = 0;
				this.bindDate(this.currPeriod);
			},
			/**
			 * Thực hiện đưa dữ liệu tìm kiếm ra component cha
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			filter() {
				this.filterPopupState = false;

				var mention;
				switch (this.currMention) {
					case 0:
						mention = "";
						break;
					case 1:
						mention = 1;
						break;
					case 2:
						mention = 0;
						break;
				}

				this.$emit("input", {
					mention: mention,
					toDate: this.currToDate.substring(0, 10),
					formDate: this.currFromDate.substring(0, 10),
					type: this.currType - 1
				});
			},
			/**
			 * Cập nhật ngày
			 * @param {Number} value
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			bindDate(value) {
				var newDates = this.changeDatefromDropdown(
					this.filterData.period[value]["key"]
				);
				this.currFromDate = newDates[0];
				this.currToDate = newDates[1];
			},
		},
		watch: {
			/**
			 * Thay đổi giai đoạn thì cập nhật ngày tương ứng
			 * @param {Number} value
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			currPeriod: function(value) {
				this.bindDate(value);
			},
		},
	};
</script>
<style>
	@import url("../../css/base/filter.css");
</style>
