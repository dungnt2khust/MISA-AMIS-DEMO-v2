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
						<base-checkbox
							:state="data['IsCustomer']"
							v-model="data['IsCustomer']"
							label="Là khách hàng"
						/>
						<base-checkbox
							:state="data['IsSupplier']"
							v-model="data['IsSupplier']"
							label="Là nhà cung cấp"
						/>
					</div>
				</div>
				<div class="form__action">
					<div
						v-on="tooltipListeners('Giúp (F1)')"
						@click="help()"
						class="form__help"
					></div>
					<div
						v-on="tooltipListeners('Đóng (Esc)')"
						@click="cancelForm()"
						class="form__cancel"
					></div>
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
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-40"
						:tabindex="1"
					/>
					<base-input
						label="Tên"
						:required="true"
						:value="data['FullName']"
						v-model="data['FullName']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-60"
						:tabindex="2"
					/>
				</div>
				<div class="form__body-item fx">
					<base-input-date
						label="Ngày sinh"
						:value="data['DateOfBirth']"
						v-model="data['DateOfBirth']"
						class="w-40"
						:tabindex="5"
					/>
					<base-radio
						label="Giới tính"
						:options="['Nam', 'Nữ', 'Khác']"
						:value="data['Gender']"
						v-model="data['Gender']"
						:tabIdx="6"
					/>
				</div>
				<div class="form__body-item">
					<base-combobox
						label="Đơn vị"
						:required="true"
						:comboboxData="departmentData"
						field="Department"
						:value="data['DepartmentId']"
						v-model="data['DepartmentId']"
						:formState="formState"
						:validateState="validateState"
						@inputError="inputErrorHandle($event)"
						:passIndex="false"
						:tabIdx="3"
					/>	
				</div>
				<div class="form__body-item fx">
					<base-input
						label="Số CMND"
						:value="data['IdentityNumber']"
						v-model="data['IdentityNumber']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-60"
						:tabindex="7"
					/>
					<base-input-date
						label="Ngày cấp"
						:value="data['IdentityDate']"
						v-model="data['IdentityDate']"
						class="w-40"
						:tabindex="8"
					/>
				</div>
				<div class="form__body-item">
					<base-input
						label="Chức danh"
						:value="data['PositionName']"
						v-model="data['PositionName']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						:tabindex="4"
					/>
				</div>
				<div class="form__body-item">
					<base-input
						label="Nơi cấp"
						:value="data['IdentityPlace']"
						v-model="data['IdentityPlace']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						:tabindex="9"
					/>
				</div>
				<div class="form__body-item fx-100">
					<base-input
						label=" Địa chỉ"
						:value="data['Address']"
						v-model="data['Address']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						:tabindex="10"
					/>
				</div>
				<div class="form__body-item fx fx-100">
					<base-input
						label="ĐT di động"
						:value="data['PhoneNumber']"
						v-model="data['PhoneNumber']"
						field="PhoneNumber"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="11"
					/>
					<base-input
						label="ĐT cố định"
						:value="data['LandlineNumber']"
						v-model="data['LandlineNumber']"
						field="PhoneNumber"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="12"
					/>
					<base-input
						label="Email"
						:value="data['Email']"
						v-model="data['Email']"
						field="Email"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="13"
					/>
				</div>
				<div class="form__body-item fx fx-100">
					<base-input
						label="Tài khoản ngân hàng"
						:value="data['BankAccount']"
						v-model="data['BankAccount']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="14"
					/>
					<base-input
						label="Tên ngân hàng"
						:value="data['BankName']"
						v-model="data['BankName']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="15"
					/>
					<base-input
						label="Chi nhánh"
						:value="data['BankBranch']"
						v-model="data['BankBranch']"
						:validateState="validateState"
						:formState="formState"
						@inputError="inputErrorHandle($event)"
						class="w-25"
						:tabindex="16"
					/>
				</div>
			</div>
			<div class="form__footer">
				<div
					v-on="tooltipListeners('Huỷ (Esc)')"
					@click="cancelForm()"
					class="form__button-cancel button"
				>
					Huỷ
				</div>
				<div class="form__control">
					<div
						v-on="tooltipListeners('Cất (Ctrl + S)')"
						@click="store(0)"
						class="button"
					>
						Cất
					</div>
					<div
						v-on="tooltipListeners(' Cất và thêm (Ctrl + Shift + S)')"
						@click="store(1)"
						class="button button--green"
					>
						Cất và thêm
					</div>
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
	import employeeAPI from "../../../js/components/employeeAPI";
	import baseAPI from "../../../js/base/baseAPI";
	import methods from "../../../mixins/methods.js";

	// COMPONENTS
	import BaseInput from "../../Base/BaseInput.vue";
	import BaseInputDate from "../../Base/BaseInputDate.vue";
	import BaseCombobox from "../../Base/BaseCombobox.vue";
	import BaseCheckbox from "../../Base/BaseCheckbox.vue";
	import BaseRadio from "../../Base/BaseRadio.vue";

	export default {
		name: "EmployeeDetail",
		mixins: [globalComponents, methods],
		components: {
			BaseInput,
			BaseCombobox,
			BaseInputDate,
			BaseCheckbox,
			BaseRadio,
		},
		data() {
			return {
				validateState: false,
				formState: false,
				both: false,
				errMsg: "",
				id: "",
				mode: "",
				data: {},
				keyPressTimeout: null,
				keyPresses: [],
				dataClone: {},
				departmentData: [],
				dragState: false,
				distanceX: 0,
				distanceY: 0,
				departmentAPI: new baseAPI("Departments"),
			};
		},
		computed: {
			/**
			 * Lắng nghe sự kiện trên form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			formListeners: function() {
				var posXBegin, posXEnd, posYBegin, posYEnd;
				return Object.assign({}, this.$listener, {
					// Nhấn xuống thì đặt vị trí bắt đầu và bật mode drag
					mousedown: (event) => {
						if (event.target.tagName != "INPUT") {
							this.dragState = true;
							posXBegin = event.clientX;
							posYBegin = event.clientY;
						}
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
							posXEnd = event.clientX;
							posYEnd = event.clientY;
							this.distanceY = posYEnd - posYBegin;
							this.distanceX = posXEnd - posXBegin;
						}
					},
				});
			},
			/**
			 * Đặt vị trí cho form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			positionOfForm() {
				return { top: this.distanceY + "px", left: this.distanceX + "px" };
			},
		},
		created() {
			/**
			 * Lắng nghe sự kiện hiển thị form
			 * @param {object} data
			 * CreatedBy: NTDUNG (29/08/2021)
			 */
			this.$bus.$on("showForm", (data) => {
				// Reset lỗi
				this.errMsg = "";
				// Gán id và mode cho form
				this.id = data.id;
				this.mode = data.mode;
				// this.data = data.data;
				// Lấy dữ liệu cho combobox
				this.getComboboxData();
				// Bind dữ liệU lên form
				this.bindFormData(data);
				// Hiển thị form
				this.showForm();
			});
			/**
			 * Bắt sự kiện keydown
			 */
			document.addEventListener("keydown", (event) => {
				if (this.formState) {
					switch (event.key) {
						case "Escape":
							event.preventDefault();
							this.cancelForm();
							this.shortKeyDelay(0);
							break;
						case "F1":
							event.preventDefault();
							this.help();
							this.shortKeyDelay(0);
							break;
						case "Control":
							event.preventDefault();
							this.keyPresses["Control"] = true;
							this.shortKeyDelay(500);
							break;
						case "Shift":
							event.preventDefault();
							if (this.keyPresses["Control"]) {
								this.keyPresses["Shift"] = true;
								this.shortKeyDelay(500);
							} else this.shortKeyDelay(0);
							break;
						default:
							if (event.key == "S") {
								if (this.keyPresses["Control"]) {
									event.preventDefault();
									this.store(1);
								}
								this.shortKeyDelay(0);
							}
							if (event.key == "s") {
								if (this.keyPresses["Control"]) {
									event.preventDefault();
									this.store(0);
								}
								this.shortKeyDelay(0);
							}
							break;
					}
				}
			});
		},
		methods: {
			/**
			 * Bind dữ liệu lên form
			 * @param {Object} data
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			bindFormData(data) {
				switch (this.mode) {
					case "UPDATE":
						// Lấy dữ liệu đẩy lên form
						this.getData(this.id);
						break;
					case "ADD":
						// Xoá dữ liệu cũ
						for (let prop in this.data) {
							this.$set(this.data, prop, null);
							// Xoá key là Id
							if (prop.includes("Id")) delete this.data[prop];
						}
						// Lấy mã mới
						this.getNewCode();
						break;
					case "CLONE":
						this.data = { ...data.data };
						// Lấy mã mới
						this.getNewCode();
						break;
					default:
				}
			},
			/**
			 * Lấy dữ liệu đưa lên form
			 * @param {string} id
			 * CreatedBy: NTDUNG (30/08/2021)
			 */
			getData(id) {
				employeeAPI
					.getById(id)
					.then((res) => {
						// Gán dữ liệu vào mảng chứa
						this.data = res.data;
						// Gán dữ liệu clone
						this.dataClone = { ...this.data };
						// Focus first input
						this.$refs.InputCode.$el.lastChild.focus();
					})
					.catch((res) => {
						this.callDialog("error", res.Response.data);
					});
			},
			/**
			 * Lấy mã mới
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			getNewCode() {
				// this.mode = "ADD";

				employeeAPI
					.getNewEmployeeCode()
					.then((res) => {
						// Gán code mới vào mảng
						this.$set(this.data, "EmployeeCode", res.data.Data);
						if (this.mode == "ADD")
							// Gán dữ liệu clone
							this.dataClone = { ...this.data };
						else 
							this.dataClone = {};
						this.mode = "ADD";
						// Focus first input
						this.$nextTick(() => {
							this.$refs.InputCode.$el.lastChild.focus();
						})
					})
					.catch((res) => {
						// Show lỗi người dùng
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
			 * Lấy dữ liệu đổ vào combobox
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			getComboboxData() {
				this.departmentAPI
					.getAll()
					.then((res) => {
						// Gán dữ liệu vào mảng chứa
						this.departmentData = res.data;
					})
					.catch((res) => {
						// Show lỗi người dùng
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
			 * Xử lý sự kiện ẩn form
			 * CreatedBy: NTDUNG (31/08/2021)
			 * ModifiedBy: NTDUNG (03/09/2021)
			 */
			cancelForm() {
				var dataChanged = this.compareObjects(this.data, this.dataClone);
				// Kiểm tra dữ liệu có thay đổi chưa
				if (!dataChanged) {
					// Gọi đến dialog
					this.callDialog(
						"confirmCancel",
						this.$resourcesVN.DataChanged
					).then((answer) => {
						// YES
						if (answer == "YES") this.store();
						// NO
						else if (answer == "NO") this.hideForm();
					});
					// Dữ liệu không thay đổi thì tắt form luôn
				} else {
					this.hideForm();
				}
			},
			/**
			 * Ẩn form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			hideForm() {
				this.formState = false;
			},
			/**
			 * Hiện form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			showForm() {
				this.formState = true;
			},
			/**
			 * Cập nhật thông tin
			 * @param {Number} modeStore : 0 - Cất, 1 - Cất và thêm
			 * CreatedBy: NTDUNG (31/08/2021)
			 * ModifiedBy: NTDUNG (02/09/2021)
			 */
			store(modeStore) {
				// Validate form
				this.validateForm();

				// Chờ validate lại
				setTimeout(() => {
					// Khi vẫn còn lỗi
					if (this.checkValidForm()) {
						// Khi thay đổi dữ liệu thì thực hiện
						if (!this.compareObjects(this.data, this.dataClone))
							// Repository theo modestore
							this.repository(modeStore);
						else this.afterStore(modeStore);
					} else {
						if (this.errMsg)
							this.callDialog("error", this.errMsg).then((answer) => {
								if (answer == "") this.errMsg = "";
							});
					}
				}, 50);
			},
			/**
			 * Hàm tương tác thêm xoá
			 * @param {Number} modeStore: 0 - Cất, 1 - Cất và thêm
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			repository(modeStore) {
				switch (this.mode) {
					case "ADD":
						this.$bus.$emit("showLoading");
						employeeAPI
							.post(this.data)
							.then(() => {
								this.afterStore(modeStore);
								// Load lại dữ liệu
								this.$bus.$emit("reloadData");
								this.$bus.$emit("showToastMessage", {message: this.$resourcesVN.AddSuccess, duration: 3000});	
							})
							.catch((res) => {
								this.$bus.$emit("hideLoading");
								// Báo lỗi
								if (res.response.data)
									this.callDialog("warn", res.response.data.Msg);
								else 
									this.callDialog("error", res.response);
							});
						break;
					case "UPDATE":
						this.$bus.$emit("showLoading");
						employeeAPI
							.put(this.id, this.data)
							.then(() => {
								this.$bus.$emit("showToastMessage", {message: this.$resourcesVN.UpdateSuccess, duration: 3000});	
								this.afterStore(modeStore);
								// Load lại dữ liệu
								this.$bus.$emit("reloadData");
							})
							.catch((res) => {
								this.$bus.$emit("hideLoading");
								// Báo lỗi
								if (res.response.data)
									this.callDialog("warn", res.response.data.Msg);
								else 
									this.callDialog("error", res.response);
							});
						break;
				}
			},
			/**
			 * Thực hiện sau khi cất
			 * @param {Number} modeStore : 0 - Cất, 1 - Cất và thêm
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			afterStore(modeStore) {
				// Cất và thêm
				if (modeStore) {
					this.mode = "ADD";
					this.bindFormData();
				}
				// Cất
				else this.hideForm();
			},
			/**
			 * Hàm kiểm tra form đã đúng chưa (nếu có một input là error thì chưa hợp lệ
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			checkValidForm() {
				var check = true;
				var inputs = this.$el.querySelectorAll("input");
				inputs.forEach((input) => {
					if (input.classList.contains("border-error")) {
						check = false;
					}
				});
				var comboboxs = this.$el.querySelectorAll(".combobox");
				comboboxs.forEach(combobox => {
					if (combobox.classList.contains("combobox--error")) {
						check = false;
					}
				})
				return check;
			},
			/**
			 * Bắt sự kiện input báo lỗi (lấy lỗi đầu tiên)
			 * @param {String} errMsg thông báo lỗi
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			inputErrorHandle(errMsg) {
				// nếu đã tồn tại lỗi thì không lấy lỗi sau nữa
				if (!this.errMsg) {
					this.errMsg = errMsg;
				}
			},
			/**
			 * Bật validate từng input
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			validateForm() {
				// Clear lỗi
				this.errMsg = "";
				// Đổi giá trị validate để input watch
				this.validateState = !this.validateState;
			},
			/**
			 * Hàm trợ giúp
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			help() {
				this.callDialog("warn", this.$resourcesVN.FeatureNotAvaiable);
			},
			/**
			 * Hàm xử lý delay phím tắt
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			shortKeyDelay(duration) {
				clearTimeout(this.keyPressTimeout);
				this.keyPressTimeout = setTimeout(() => {
					this.keyPresses = [];
				}, duration);
			},
		},
	};
</script>
<style>
	@import url('../../../css/base/form.css');
</style>
