<template lang="">
	<div v-show="formState" class="form">
		<div
			ref="form"
			v-on="formListeners"
			:style="positionOfForm"
			class="form__main"
		>
			<div class="form__header">
				<div class="form__header-content">
					<div class="form__title">Thông tin nhân viên</div>
					<div class="form__checklist fx">
						<base-checkbox label="Là khách hàng" />
						<base-checkbox label="Là nhà cung cấp" />
					</div>
				</div>
				<div class="form__action">
					<div class="form__help"></div>
					<div @click="hideForm()" class="form__cancel"></div>
				</div>
			</div>
			<div class="form__body">
				<div class="form__body-item fx">
					<base-input
						label="Mã"
						ref="InputCode"
						:required="true"
						:value="data['EmployeeCode']"
						v-model="data['EmployeeCode']"
						class="w-40"
					/>
					<base-input
						label="Tên"
						:required="true"
						:value="data['FullName']"
						v-model="data['FullName']"
						class="w-60"
					/>
				</div>
				<div class="form__body-item fx">
					<base-input-date
						label="Ngày sinh"
						:value="data['DateOfBirth']"
						v-model="data['DateOfBirth']"
						class="w-40"
					/>
					<base-radio
						label="Giới tính"
						:options="['Nam', 'Nữ', 'Khác']"
						:value="data['Gender']"
						v-model="data['Gender']"
					/>
				</div>
				<div class="form__body-item">
					<base-combobox
						label="Đơn vị"
						:required="true"
						:comboboxData="comboboxData"
						field="Department"
						:value="data['DepartmentId']"
						v-model="data['DepartmentId']"
					/>
				</div>
				<div class="form__body-item fx">
					<base-input
						label="Số CMND"
						:value="data['IdentityNumber']"
						v-model="data['IdentityNumber']"
						class="w-60"
					/>
					<base-input-date
						label="Ngày cấp"
						:value="data['IdentityDate']"
						v-model="data['IdentityDate']"
						class="w-40"
					/>
				</div>
				<div class="form__body-item">
					<base-input
						label="Chức danh"
						:value="data['PossitionName']"
						v-model="data['PossitionName']"
					/>
				</div>
				<div class="form__body-item">
					<base-input
						label="Nơi cấp"
						:value="data['IdentityPlace']"
						v-model="data['IdentityPlace']"
					/>
				</div>
				<div class="form__body-item fx-100">
					<base-input
						label=" Địa chỉ"
						:value="data['Address']"
						v-model="data['Address']"
					/>
				</div>
				<div class="form__body-item fx fx-100">
					<base-input
						label="ĐT di động"
						:value="data['PhoneNumber']"
						v-model="data['PhoneNumber']"
						field="PhoneNumber"
						class="w-25"
					/>
					<base-input
						label="ĐT cố định"
						:value="data['LandlineNumber']"
						v-model="data['LandlineNumber']"
						class="w-25"
					/>
					<base-input
						label="Email"
						:value="data['Email']"
						field="Email"
						class="w-25"
					/>
				</div>
				<div class="form__body-item fx fx-100">
					<base-input
						label="Tài khoản ngân hàng"
						:value="data['BankAccount']"
						v-model="data['BankAccount']"
						class="w-25"
					/>
					<base-input
						label="Tên ngân hàng"
						:value="data['BankName']"
						v-model="data['BankName']"
						class="w-25"
					/>
					<base-input
						label="Chi nhánh"
						:value="data['BankBranch']"
						v-model="data['BankBranch']"
						class="w-25"
					/>
				</div>
			</div>
			<div class="form__footer">
				<div @click="hideForm()" class="form__button-cancel button">
					Huỷ
				</div>
				<div class="form__control">
					<div @click="update()" class="button">Cất</div>
					<div class="button button--green">Cất và thêm</div>
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import axios from "axios";
	import Mixin from "../../mixins/globalComponents/globalComponents.js";

	// COMPONENTS
	import BaseInput from "./BaseInput.vue";
	import BaseInputDate from "./BaseInputDate.vue";
	import BaseCombobox from "./BaseCombobox.vue";
	import BaseCheckbox from "./BaseCheckbox.vue";
	import BaseRadio from "./BaseRadio.vue";

	export default {
		name: "BaseForm",
		mixins: [Mixin],
		components: {
			BaseInput,
			BaseCombobox,
			BaseInputDate,
			BaseCheckbox,
			BaseRadio,
		},
		data() {
			return {
				formState: false,
				data: [],
				comboboxData: [],
				dragState: false,
				dragX: 0,
				dragY: 0,
			};
		},
		computed: {
			formListeners: function() {
				var dragXBegin, dragXEnd, dragYBegin, dragYEnd;
				return Object.assign({}, this.$listener, {
					// Nhấn xuống thì đặt vị trí bắt đầu và bật mode drag
					mousedown: (event) => {
						this.dragState = true;
						dragXBegin = event.clientX;
						dragYBegin = event.clientY;
					},
					// Tắt mode drag khi nhấc chuột
					mouseup: () => {
						this.dragState = false;
					},
					// Tắt mode drag khi ra khỏi form
					mouseleave: () => {
						this.dragState = false;
					},
					// Khi trong mode drag thì tìm ra offset và đặt position
					mousemove: (event) => {
						if (this.dragState) {	
							// Gán vị trí mới
							dragXEnd = event.clientX;
							dragYEnd = event.clientY;
							this.dragY = dragYEnd - dragYBegin;
							this.dragX = dragXEnd - dragXBegin;
						}
					},
				});
			},
			/**
			 * Đặt vị trí cho form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			positionOfForm() {
				return { top: this.dragY + "px", left: this.dragX + "px" };
			},
		},
		created() {
			/**
			 * Lắng nghe sự kiện hiển thị form
			 * @param {object} data
			 * CreatedBy: NTDUNG (29/08/2021)
			 */
			this.$bus.$on("showForm", (data) => {
				// Lấy dữ liệU cho combobox
				this.getComboboxData();

				switch (data.mode) {
					case "UPDATE":
						// Lấy dữ liệu đẩy lên form
						this.getData(data.id);
						break;
					case "ADD":
						// Xoá dữ liệu cũ
						for (var prop in this.data) {
							this.$set(this.data, prop, null);
						}
						// Lấy mã mới
						this.getNewCode();
						break;
					default:
				}
				// Hiển thị form
				this.formState = true;
			});
		},
		methods: {
			/**
			 * Lấy dữ liệu đưa lên form
			 * @param {string} id
			 * CreatedBy: NTDUNG (30/08/2021)
			 */
			getData(id) {
				axios
					.get(`https://localhost:44342/api/v1/employees/` + id)
					.then((res) => {
						// Gán dữ liệu vào mảng chứa
						this.data = res.data;
					})
					.catch((res) => {
						console.log(res);
					});
			},
			/**
			 * Lấy mã mới
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			getNewCode() {
				axios
					.get("https://localhost:44342/api/v1/employees/NewEmployeeCode")
					.then((res) => {
						// Gán code mới vào mảng
						this.$set(this.data, "EmployeeCode", res.data.Data);
					})
					.catch((res) => {
						console.log(res);
					});
			},
			/**
			 * Lấy dữ liệu đổ vào combobox
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			getComboboxData() {
				axios
					.get("https://localhost:44342/api/v1/departments")
					.then((res) => {
						// Gán dữ liệu vào mảng chứa
						if (res.status == 200) this.comboboxData = res.data;
					})
					.catch((res) => {
						console.log(res);
					});
			},
			/**
			 * Xử lý sự kiện ẩn form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			hideForm() {
				// Gọi đến dialog
				this.callDialog(
					"confirm",
					"Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
				).then((answer) => {
					// YES
					if (answer) this.formState = false;
					// NO
					else this.formState = false;
				});
			}	
		},
		watch: {
			/**
			 * Khi form được bật thì focus vào ô đầu tiên
			 * @param {boolean} value
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			formState: function() {
				setTimeout(() => {
					// Focus first input
					this.$refs.InputCode.$el.lastChild.focus();
				}, 1);
			},
		},
	};
</script>
<style>
	@import url('../../css/base/form.css');
</style>
