<template lang="">
	<div class="input-wrapper">
		<span v-if="label != ''" class="label label--date">
			{{ label }}
			<!-- <span v-if="required" class="text-red">*</span> -->
		</span>
		<div class="input__main">
			<input
				v-on="inputDateListeners"
				type="text"
				:placeholder="placeholder != '' ? placeholder : 'DD/MM/YYYY'"
				v-model="inputValue"
				class="input input--date"
				:class="{
					'input--disable': !enable,
					'border-error': errorMsg ? true : false,
				}"
				ref="inputDate"
				:readonly="!enable"
				:title="errorMsg ? errorMsg : ''"
				v-if="!disable"
			/>
			<div v-if="disable" class="input__span--date">{{ inputValue }}</div>
			<span
				@click="toggleDatePicker()"
				class="input__icon"
				:class="{ 'input--disable': !enable }"
				v-if="!disable"
			></span>
			<base-date-picker
				:value="currDate"
				v-model="currDate"
				:datePickerState="datePickerState"
				:overtoday="overtoday"
				@hideDatepicker="datePickerState = false"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import methods from "../../mixins/methods";
	import globalComponents from "../../mixins/globalComponents/globalComponents.js";
	// COMPONENTS
	import BaseDatePicker from "./BaseDatePicker.vue";

	export default {
		name: "BaseInputDate",
		mixins: [methods, globalComponents],
		components: {
			BaseDatePicker,
		},
		props: {
			label: {
				type: String,
				default: "",
			},
			required: {
				type: Boolean,
				default: false,
			},
			value: {
				type: String,
				default: null,
			},
			overtoday: {
				type: Boolean,
				default: false,
			},
			enable: {
				type: Boolean,
				default: true,
			},
			disable: {
				type: Boolean,
				default: false,
			},
			placeholder: {
				type: String,
				default: "",
			},
			formName: {
				type: String,
				default: "",
			},
			name: {
				type: String,
				default: "",
			},
			range: {
				type: Object,
				default: null,
			},
		},
		data() {
			return {
				datePickerState: false,
				currDate: null,
				firstFocus: false,
				inputValue: "",
				errorMsg: "",
			};
		},
		computed: {
			/**
			 * Lắng nghe sự kiện input
			 * @return {object}
			 * CreatedBy: NTDUNG (30/08/2021)
			 */
			inputDateListeners: function() {
				return Object.assign({}, this.$listener, {
					blur: () => {
						this.firstFocus = false;
						this.validateInput();
					},
					focus: (event) => {
						if (!this.firstFocus) {
							event.target.select();
							this.firstFocus = true;
						}
					},
					input: () => {
						this.currDate = null;
						// var type = event.inputType;
						// if (type == "deleteContentBackward")
						// {
						// 	event.preventDefault();
						// 	this.formatInputDate(type);
						// } else {
						// 	this.formatInputDate();
						// }
					},
					keydown: (event) => {
						var charCode = event.key.charCodeAt(0);
						var key = event.key;
						if (
							(charCode < 48 || charCode > 57) &&
							key != "Backspace" &&
							key != "F1" &&
							key != "Tab" &&
							key != "Shift"
						) {
							event.preventDefault();
						}
					},
				});
			},
		},
		created() {
			this.$bus.$on("validate" + this.formName, () => {
				this.validateInput();
				if (this.errorMsg) {
					console.log("send error");
					this.$bus.$emit(
						"catchError" + this.formName,
						this.errorMsg,
						this.$refs.inputDate
					);
				}
			});
		},
		methods: {
			/**
			 * Xử lý giá trị để in lên input
			 * @return {string}
			 */
			resolvedValue() {
				// Nếu có ngày thì hiển thị
				if (this.currDate) {
					this.inputValue = this.formatDate(this.currDate);
				} else {
					this.inputValue = "";
				}
			},
			/**
			 * Bật tắt datepicker
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			toggleDatePicker() {
				if (this.enable) this.datePickerState = !this.datePickerState;
			},
			/**
			 * Validate Input
			 * CreatedBy: NTDUNG (04/10/2021)
			 */
			validateInput() {
				this.errorMsg = "";
				if (this.required) {
					if (!this.value)
						if (this.label)
							this.errorMsg = this.formatString(
								this.$resourcesVN.NOTIFY.FieldRequired,
								this.label
							);
						else
							this.errorMsg = this.formatString(
								this.$resourcesVN.NOTIFY.FieldRequired,
								this.name
							);
					else {
						if (this.range) {
							var currDate = this.formatDate(this.value);
							var fromDate = this.formatDate(this.range.from.Date);

							if (new Date(currDate) < new Date(fromDate)) {
								this.errorMsg = this.formatString(
									this.$resourcesVN.NOTIFY.MustThanDate,
									this.label,
									this.range.from.Field
								);
							}
						}
					}
				}
				// var newDate = this.dateReverse(this.inputValue);
				// if (newDate) {
				// 	this.currDate = newDate;
				// }
			},
			// /**
			//  * Định dạng lại text trên input
			//  * CreatedBy: NTDUNG (29/09/2021)
			//  */
			// formatInputDate(type) {
			// 	var input = this.inputValue.replaceAll('/', '');
			// 	input = input.replaceAll('_', '');
			// 	if (input.length > 8) input = input.slice(0, -1);
			// 	console.log(input);
			// 	if (type == "deleteContentBackward" && input && input.length <= 7) input = input.slice(0, -1);
			// 	var formatedInput = '__/__/____';
			// 	input[0] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 0, input[0]) : '';
			// 	input[1] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 1, input[1]) : '';
			// 	input[2] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 3, input[2]) : '';
			// 	input[3] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 4, input[3]) : '';
			// 	input[4] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 6, input[4]) : '';
			// 	input[5] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 7, input[5]) : '';
			// 	input[6] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 8, input[6]) : '';
			// 	input[7] ? formatedInput = this.replaceCharacterByIndex(formatedInput, 9, input[7]) : '';
			// 	this.inputValue = formatedInput;
			// }
		},
		watch: {
			/**
			 * Sự kiện khi đã pick được 1 ngày mới
			 * @param {String} newDate
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			currDate: function(newDate) {
				if (newDate) {
					this.$emit("input", `${newDate}T00:00:00`);
				} else this.$emit("input", null);
			},
			/**
			 * Bắt sự kiện thay đổi value thì cập nhật ngày hiện tại
			 * @param {String} newValue
			 * CreatedBy: NTDUNG (02/09/2021)
			 */

			value: {
				handler(newValue) {
					this.errorMsg = "";
					if (newValue) {
						this.currDate = newValue.substring(0, 10);
						this.resolvedValue();
					} else {
						this.currDate = null;
						this.resolvedValue();
					}
				},
				immediate: true,
			},
		},
	};
</script>
<style>
	@import url("../../css/base/inputdate.css");
</style>
