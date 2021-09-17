<template lang="">
	<div class="input-wrapper">
		<span class="label">
			{{ label }} <span v-if="required" class="text-red">*</span></span
		>
		<input
			v-on="inputListeners"
			:title="isError ? errorMsg : ''"
			:value="value"
            :class="{'border-error': isError}"
			type="text"
			class="input"
            :tabindex="tabindex"
		/>
	</div>
</template>
<script>
    // LIBRARY
    import globalComponents from '../../mixins/globalComponents/globalComponents.js'

	export default {
		name: "BaseInput",
        mixins: [globalComponents],
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
				default: "",
			},
            field: {
                type: String, 
                default: ""
            },
            validateState: {
                type: Boolean,
                default: false
            },
            formState: {
                type: Boolean,
                default: false
            },
            tabindex: {
                type: Number,
                default: -1
            }
		},
		data() {
			return {
				isError: false,
				errorMsg: '',
                type: 0,
                firstFocus: false
			};
		},
        computed: {
            /**
             * Lắng nghe các sự kiện của input
             * CreatedBy: NTDUNG (31/08/2021)
             */
            inputListeners: function() {
                return Object.assign({}, this.$listener, {
                    input: () => {
                        // Khi nhập thì bỏ hiện lỗi đi
                        this.isError = false;
                        this.errorMsg = '';
                    },
                    blur: (event) => {
                        // Đưa dữ liệu ra phía cha
                        this.$emit('input', event.target.value);
                        // Validate dữ liệu
                        this.validateInput(event.target.value, 0);
                        // Tắt mode focus
                        this.firstFocus = false
                    },
                    focus: (event) => {
                        if (!this.firstFocus) {
                            // Chọn text trong input
                            event.target.select();
                            // Bật mode focus
                            this.firstFocus = true;
                        }
                    }
                });
            }
        },
        methods: {
            /**
             * validate các ô input
             * @param {String} value
             * @param {Number} type kiểu show lỗi 0: chỉ blur, 1: blur và show dialog
             * CreatedBy: NTDUNG (05/08/2021)
             * ModifiedBy: NTDUNG (06/08/2021)
             */
            validateInput(value, type) {
                // Đặt kiểu show
                this.type = type;

                if (value === null || value === '') {
                    if (this.required) {
                        this.errorMsg = this.$resourcesVN.RequireField.replace('@', this.label);
                        this.isError = true;
                    }
                } else {
                    switch (this.field) {
                        case 'Email':
                            if (!this.validEmail(value)) {
                                this.errorMsg = this.$resourcesVN.WrongFormat.replace('@', this.label);
                                this.isError = true;
                            }
                            break;
                        case 'PhoneNumber':
                            if (!this.validPhone(value)) {
                                this.errorMsg = this.$resourcesVN.WrongFormat.replace('@', this.label);
                                this.isError = true;
                            }
                            break;
                        default:
                    }
                }
            },
            /**
             * validate email đúng định dạng
             * @param {String} email xâu email người dùng nhập vào
             * @returns {Boolean} true nếu đúng định dạng
             * CreatedBy: NTDUNG (31/08/2021)
             * ModifiedBy: NTDUNG (31/08/2021)
             */
            validEmail: function (email) {
                var regEmail =
                    /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return regEmail.test(email);
            },
            /**
             * validate số điện thoại đúng định dạng
             * @param {String} phone sdt người dùng nhập vào
             * @returns {Boolean} true nếu đúng định dạng
             * CreatedBy: NTDUNG (31/08/2021)
             * ModifiedBy: NTDUNG (31/08/2021)
             */
            validPhone: function (phone) {
                var regPhone = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
                return regPhone.test(phone);
            },
        },
        watch: {
            /**
             * Khi giá trị form validate thay đổi thì validate input
             * CreatedBy: NTDUNG (02/09/2021)
             */
            validateState: function() {
                // Clear lỗi cũ
                this.isError = false;
                setTimeout(() => { 
                    this.validateInput(this.value);
                }, 10); 
            },
            /**
             * Lắng nghe khi có lỗi thì emit ra ngoài để form biết
             * @param {Boolean} state
             * CreatedBy: NTDUNG (02/09/2021)
             */
            isError: function(state) {
                // Nếu có lỗi thì emit lỗi ra ngoài
                if (state)       
                    this.$emit('inputError', `${this.errorMsg}`);
            },
            /**
             * Bắt sự kiện khi trạng thái form thay đổi
             * @param {Boolean} state
             * CreatedBy: NTDUNG (02/09/2021)
             */
            formState: function(state) {
                if (state)
                    this.isError = false;
            }
        }
	};
</script>
<style>
    @import url('../../css/base/input.css');
</style>
