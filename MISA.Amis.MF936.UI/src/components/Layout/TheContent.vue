<template lang="">
	<div id="content">
		<div class="content__header">
			<div class="content__name">
                Khách hàng
			</div>
			<div
				@click="contentAddOnClick()"
				class="button button--green button--large"
			>
				Thêm mới khách hàng
			</div>
		</div>
		<div class="table-wrapper" :class="{ 'table--selectmany':  selectedRows.length > 1}">
			<div class="table__control">
				<div
					@click="controlMany()"
					class="table__control-many non-select-text"
				>
					<div class="table__control-many-label">
						Thực hiện hàng loạt	
					</div>
					<ul v-if="tableSelectMany" class="table__control-list">
						<li @click.stop="deleteMany()" class="table__control-item">
							Xoá
						</li>
					</ul>
				</div>
				<div class="table__control-query">
					<div class="table__search">
						<input
							v-model="filterString"
							@input="searchOnInput()"
							ref="inputSearch"
							type="text"
							class="table__search-input"
							placeholder="Tìm theo mã, họ tên, số điện thoại"
						/>
					</div>
					<div
						@click="changePageInfo({ filterString: '' })"
						v-on="tooltipListeners('Tải lại dữ liệu')"
						class="table__reload"
					>
						<div class="table__reload-icon"></div>
					</div>
					<div
						v-on="tooltipListeners('Xuất file Excel')"
						@click="exportData()"
						class="table__export"
					>
						<div class="table__export-icon"></div>
					</div>
				</div>
			</div>
			<base-table
				:tableId="tableId"
				:tableStyle="tableCustomerStyle"
				:tableData="tableData"
				:tableLoading="tableLoading"
				v-model="markRows"
				:controller="controller"
			/>
			<base-pagination
				:totalPage="totalPage"
				:totalRecord="totalRecord"
				:currPage="currPage"
				:currOption="currOption"
				:paginationData="paginationData"
				@changePageInfo="changePageInfo($event)"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import customerAPI from "../../../js/components/customerAPI"
	import Table from "../../../Mixins/table.js"
	import listeners from "../../../Mixins/listeners.js"
	import methods from '../../../Mixins/methods.js'

	// COMPONENT
	import BaseTable from "../../Base/BaseTable.vue"
	import BasePagination from "../../Base/BasePagination.vue"

	export default {
		name: "CustomerPage",
		mixins: [Table, listeners, methods],
		components: {
			BaseTable,
			BasePagination,
		},
		data() {
			return {
				inputTimeout: null,
				filterString: "",
				markRows: [],
				tableData: [],
				tableSelectMany: false,
				tableId: "CustomerId",
				controller: "Customers",
				tableLoading: false,
				totalPage: 0,
				totalRecord: 0,
				currPage: 1,
				currOption: 0,
				paginationData: [
					{ PaginationId: 20, PaginationName: "20 bản ghi trên 1 trang" },
					{ PaginationId: 30, PaginationName: "30 bản ghi trên 1 trang" },
					{ PaginationId: 40, PaginationName: "40 bản ghi trên 1 trang" },
					{ PaginationId: 50, PaginationName: "50 bản ghi trên 1 trang" },
					{ PaginationId: 100, PaginationName: "100 bản ghi trên 1 trang" },
				],
			};
		},
		created() {
			this.$bus.$on("reloadData", () => {
				this.getTableData(this.currOption, this.currPage, this.filterString);
			});
		},
		mounted() {
			// Lấy dữ liệu
			this.getTableData(this.currOption, this.currPage, this.filterString);	
		},
		computed: {
			/**
			 * Thông tin pagination
			 * @returns {Object}
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			currInfo() {
				return {
					currPage: this.currPage,
					currOption: this.currOption,
				};
			},
			/**
			 * Những dòng đã được chọn
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			selectedRows() {
				var rows = [];
				this.markRows.forEach((row, index) => {
					if (row) 
						rows.push(this.tableData[index][this.tableId]);
				});
				return rows;
			}
		},
		methods: {
			/**
			 * Click vào nút Thực hiện hàng loạt
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			controlMany() {
				if (this.selectedRows.length > 1) 
					this.tableSelectMany = !this.tableSelectMany;
			},
			/**
			 * Sự kiện nhập vào input search
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			searchOnInput() {
				// Bỏ timeout
				clearTimeout(this.inputTimeout);
				// Đặt timeout nếu sau 500ms không nhập thì chạy
				this.inputTimeout = setTimeout(() => {
					this.changePageInfo({});
				}, 500);
			},
			/**
			 * Thông tin thay đổi - load lại dữ liệu
			 * @param {Object} data
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			changePageInfo(data) {
				// Nếu dữ liệu truyền vào tồn tại thì đổi nếu không thì giữ nguyên
				this.currOption = data["currOption"]
					? data["currOption"]
					: this.currOption;
				this.currPage = data["currPage"] ? data["currPage"] : this.currPage;
				this.filterString =
					data["filterString"] !== undefined
						? data["filterString"]
						: this.filterString;

				// Khi filterString khác rỗng thì khi load lại dữ liệu sẽ về trang đầu
				if (this.filterString != "") {
					this.currPage = 1;
				}
				// Lấy dữ liệu
				this.getTableData(this.currOption, this.currPage, this.filterString);
			},
			/**
			 * Lấy dữ liệu đổ vào table
			 * @param {Number} currOption
			 * @param {Number} currPage
			 * @param {String} filterString
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			getTableData(currOption, currPage, filterString) {
				// Bật loading
				this.turnOnLoading();
				// Tắt báo lỗi đi
				this.tableError = false;
				// Lấy dữ liệu bảng từ API
				customerAPI
					.getCustomerFilter(
						this.paginationData[currOption]["PaginationId"],
						currPage,
						filterString
					)
					.then((res) => {
						// Gán dữ liệu vào biến prop sang BaseTable
						this.tableData = res.data.Data;
						this.totalPage = res.data.TotalPage;
						this.totalRecord = res.data.TotalRecord;
						// Tắt loading
						this.tableLoading = false;
						this.$bus.$emit('hideLoading');
						// Focus ô tìm kiếm
						this.$refs.inputSearch.focus();
					})
					.catch((res) => {
						// Tắt loading
						this.tableLoading = false;
						this.tableData = [];
						// Show lỗi
						this.showError(res);
					});
			},
			/**
			 * export dữ liệu nhân viên
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			exportData() {
				// Gọi đến API export
				customerAPI
					.export()
					.then((res) => {
						// Tạo một thẻ a
						const link = document.createElement("a");
						// Gán link để download cho thẻ ạ
						link.href = window.URL.createObjectURL(new Blob([res.data]));
						// Tạo thuộc tính tên tải xuống cho link
						link.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
						// Thêm vào trang hiện tại
						document.body.appendChild(link);
						// Nhấn vào link
						link.click();
					})
					.catch((res) => {
						// Show lỗi cho người dùng
						this.callDialog("error", res.response.data.userMsg);
						// Show lỗi dev
						console.log({
							devMsg: res.response.data.devMsg,
							errorCode: res.response.data.errorCode,
							traceId: res.response.data.traceId,
						});
					});
			},
			/**
			 * Sự kiện nhấn vào nút Add
			 * CreatedBy: NTDUNG (29/08/2021)
			 */
			contentAddOnClick() {
				this.$bus.$emit("showForm", { mode: "ADD" });
			},
			/**
			 * Bật loading
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			turnOnLoading() {
				// Hiện loading
				this.tableLoading = true;	
			},
			/**
			 * Xoá nhiều
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			deleteMany() {
				this.tableSelectMany = false;
				// Hiển thị cảnh báo 
				this.callDialog('confirm', 'Bạn có thật sự muốn xoá những dòng đã chọn')
				.then(answer => {
					if (answer == 'YES')
						customerAPI.deleteMany(this.selectedRows)
						.then(() => {
							// Tải lại dữ liệu
							this.getTableData(this.currOption, this.currPage, this.filterString);
						})
						.catch((res) => {
							// Show lỗi
							this.callDialog('error', res.response.data.userMsg);
						})
				})
			},
		},
		watch: {
			/**
			 * Khi số dòng nhỏ hơn 2 thì ẩn đi
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			markRows: function() {
				if (this.selectedRows.length < 2) 
					this.tableSelectMany = false;
			}
		}
	};
</script>
<style lang=""></style>
