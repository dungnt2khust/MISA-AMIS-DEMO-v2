<template lang="">
	<div class="pagination">
		<div class="pagination__desc">
			Tổng số: <span class="text-bold"> {{ totalRecord }}</span> bản ghi
		</div>
		<div class="pagination__control">
			<base-combobox
				:comboboxData="paginationData"
				field="Pagination"
				class="pagination__select"
				comboboxType="up"
				:value="currOptionTranfer"
				v-model="currOptionTranfer"
			/>
			<div class="pagination__redirect">
				<div
					class="pagination__prev non-select-text"
					:class="{ 'pagination--disable': currPageTranfer == 1 }"
					@click="paginationPrevOnClick()"
				>
					Trước
				</div>
				<div
					v-for="index in totalPage"
					v-show="displayItems(index)"
					class="pagination__index non-select-text"
					:class="{ 'pagination__index--selected': index == currPageTranfer }"
					@click="currPageTranfer = index"
					:key="index"
				>
					{{ index }}
				</div>
				<div
					class="pagination__next non-select-text"
					:class="{ 'pagination--disable': currPageTranfer == totalPage }"
					@click="paginationNextOnClick()"
				>
					Sau
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseCombobox from "./BaseCombobox.vue";

	export default {
		name: "BasePagination",
		components: {
			BaseCombobox,
		},
		props: {
			totalPage: {
				type: Number,
				default: 0,
			},
			totalRecord: {
				type: Number,
				default: 0,
			},
			currPage: {
				type: Number,
				default: 0,
			},
			currOption: {
				type: Number,
				default: 0,
			},
			paginationData: {
				type: Array,
				default: function() {
					return [];
				},
			},
		},
		data() {
			return {
				numDisplayPage: 4,
				currPageTranfer: this.currPage,
				currOptionTranfer: this.currOption,
			};
		},
		methods: {
			/**
			 * Hiển thị các item của pagination
			 * @param {Number} index
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			displayItems(index) {
				// Làm tròn
				var idxDisplay = Math.ceil(this.currPageTranfer / this.numDisplayPage);

				if (this.totalPage - this.currPage <= this.numDisplayPage - 1) {
					return (
						index <= this.totalPage &&
						index >= this.totalPage - this.numDisplayPage + 1
					);
				}
				return (
					index <= idxDisplay * this.numDisplayPage &&
					index >= (idxDisplay - 1) * this.numDisplayPage + 1
				);
			},
			/**
			 * Xử lý sự kiện click vào nút Prev
			 * CreatedBy: NTDUNG (28/08/2021)
			 */
			paginationPrevOnClick() {
				if (this.currPageTranfer != 1) this.currPageTranfer--;
			},
			/**
			 * Xử lý sự kiện click vào nút Next
			 * CreatedBy: NTDUNG (28/08/2021)
			 */
			paginationNextOnClick() {
				if (this.currPageTranfer != this.totalPage) this.currPageTranfer++;
			},
		},
		watch: {
			/**
			 * Khi currPage thay đổi thì đặt lại vị trí
			 * @param {Number} index
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			currPage: function(index) {
				this.currPageTranfer = index;
			},
			/**
			 * Khi currPageTranfer thay đổi thì emit ra component cha
			 * @param {Number} index
			 * CreateBy: NTDUNG (01/09/2021)
			 */
			currPageTranfer: function(index) {
				this.$emit("changePageInfo", {
					currPage: index,
					currOption: this.currOption,
				});
			},
			/**
			 * Sự kiện khi lựa chọn thay đổi
			 * @param {Number} index
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			currOptionTranfer: function(index) {
				// Khi currOption thay đổi thì currPage set về 1
				this.$emit("changePageInfo", {
					currPage: 1,
					currOption: index,
				});
			},
		},
	};
</script>
<style lang=""></style>
