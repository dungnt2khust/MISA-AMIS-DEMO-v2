<template lang="">
	<div class="input-wrapper">
		<span class="label">
			{{ label }} <span v-if="required" class="text-red">*</span></span
		>
		<div class="input__main">
			<input
				v-on="inputDateListeners"
				type="text"
				placeholder="DD/MM/YYYY"
				v-model="inputValue"
				class="input input--date"
				:tabindex="tabindex"
			/>
			<span
				@click="datePickerState = !datePickerState"
				class="input__icon"
			></span>
			<base-date-picker
				:value="currDate"
				v-model="currDate"
				:datePickerState="datePickerState"
				@hideDatepicker="datePickerState = false"
			/>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import methods from "../../mixins/methods"
	// COMPONENTS
	import BaseDatePicker from "./BaseDatePicker.vue";

	export default {
		name: 'BaseInputDate',
		mixins: [methods],
		components: {
			BaseDatePicker,
		},
		props: {
			label: {
				type: String,
				default: '',
			},
			required: {
				type: Boolean,
				default: false,
			},
			value: {
				type: String,
				default: null
			},
			tabindex: {
				type: Number,
				default: -1
			}
		},
		data() {
			return {
				datePickerState: false,
				currDate: null,
				firstFocus: false,
				inputValue: ''
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
					},
					focus: (event) => {
						if (!this.firstFocus) {
							event.target.select();
							this.firstFocus = true;
						}
					},
					input: (event) => {
						var data = event.data;
						if (data) {
							var code = data.charCodeAt(0);
							if ((code >= 32 && code <= 47) || (code >= 58 && code <= 255)) {
								this.inputValue = this.inputValue.slice(0, -1);
							}
						}
						var number = '';
						for (let i = 0; i < this.inputValue.length; i++) {
							if (/^.*?[0-9]$/.test(this.inputValue[i])) {
								number += this.inputValue[i];
							}
						}
						console.log(number);
						var input = [];
						input[0] =  number[0] !== undefined ? number[0] : '_';
						input[1] =  number[1] !== undefined ? number[1] : '_';
						input[2] =  '/';
						input[3] =  number[2] !== undefined ? number[2] : '_';
						input[4] =  number[3] !== undefined ? number[3] : '_';
						input[5] =  '/';
						input[6] =  number[4] !== undefined ? number[4] : '_';
						input[7] =  number[5] !== undefined ? number[5] : '_';
						input[8] =  number[6] !== undefined ? number[6] : '_';
						input[9] =  number[7] !== undefined ? number[7] : '_';
						console.log(input.join(''));
						this.inputValue = input.join('');
					}
                })
            },	      
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
					this.inputValue = '';
				}
			},
		},
		watch: {
			/**
			 * Sự kiện khi đã pick được 1 ngày mới
			 * @param {String} newDate
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			currDate: function(newDate) {
				if (newDate) 
					this.$emit('input', `${newDate}T00:00:00`);
			},
			/**
			 * Bắt sự kiện thay đổi value thì cập nhật ngày hiện tại
			 * @param {String} newValue
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			value: function(newValue) {
				if (newValue) {
					this.currDate = newValue.substring(0, 10);
					this.resolvedValue();
				}
				else 
					this.currDate = null;
			}
		},
	};
</script>
<style>
	@import url('../../css/base/inputdate.css');
</style>
