<template lang="">
	<div class="input-main" :style="{'width': width}">
		<span v-if="label != ''" class="label">
			{{ label }} 
            <!-- <span v-if="required" class="text-red">*</span> -->
        </span>
        <div class="fx-center-ver">
            <input
                v-on="inputListeners"
                :title="isError ? errorMsg : ''"
                :value="value"
                :class="positionOfInput"
                class="input"
                :tabindex="tabindex" 
                :placeholder="placeholder"
                :readonly="!enable"
                :type="type"
                :style="{border: noborder ? 'none !important' : ''}"
                ref="input"
                v-if="!disable"
            />
            <div v-if="disable" :class="positionOfInput" class="input__span"> {{value}}</div>
            <span v-if="unit != ''" class="input-unit ml-10">
                {{ unit }}
            </span>
        </div>	
	</div>
</template>
<script>
    // LIBRARY
    import globalComponents from '../../mixins/globalComponents/globalComponents.js'
    import TableDataStyle from "../../js/enum/tableDataStyle.js"

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
				type: [String, Number],
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
            },
            unit: {
                type: String,
                default: ''
            },
            width: {
                type: String,
                default: ''
            },
            pos: {
                type: Number,
                default: -1
            },
            enable: {
                type: Boolean,
                default: true
            },
            disable: {
                type: Boolean, 
                default: false
            },
            intableinput: {
                type: Boolean,
                default: false
            },
            placeholder: {
                type: String,
                default: ''
            },
            type: {
                type: String,
                default: 'text'
            },
            syncfield: {
                type: String,
                default: ''
            },
            index: {
                type: Number,
                default: -1
            },
            formName: {
                type: String,
                default: ''
            },
            name: {
                type: String,
                default: ''
            },
            noborder: {
                type: Boolean,
                default: false
            } 
		},
		data() {
			return {
				isError: false,
				errorMsg: '',
                firstFocus: false
			};
		},
        created() {
            this.$bus.$on('validate' + this.formName, () => {
                this.validateInput(this.value);
                if (this.errorMsg)
                    this.$bus.$emit('catchError' + this.formName, this.errorMsg, this.$refs.input);
            })
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
                        // Emit dữ liệu để đồng bộ
                        if (this.syncfield)
                            this.$bus.$emit('change' + this.syncfield, this.index, 'INPUT', event.target.value);
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
            },
            /**
             * Vị trí của input
             * CreatedBy: NTDUNG (28/09/2021)
             */
            positionOfInput() {
                var styleClass = {"border-error": this.isError, "input--disable": !this.enable};
                switch(this.pos) {
                    case TableDataStyle.POS.Left: 
                        styleClass["text-align-left"] = true;
                        break;
                    case TableDataStyle.POS.Center: 
                        styleClass["text-align-center"] = true;
                        break;
                    case TableDataStyle.POS.Right: 
                        styleClass["text-align-right"] = true;
                        break;
                }
                return styleClass;
            }
        },
        methods: {
            /**
             * validate các ô input
             * @param {String} value
             * CreatedBy: NTDUNG (05/08/2021)
             * ModifiedBy: NTDUNG (06/08/2021)
             */
            validateInput(value) {
                this.isError = false;
                this.errorMsg = '';
                if (value === null || value === '') {
                    if (this.required) {
                        if (this.label)
                            this.errorMsg = this.$resourcesVN.RequireField.replace('@', this.label);
                        else 
                            this.errorMsg = this.$resourcesVN.RequireField.replace('@', this.name);
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
            },
            value: function() {
                this.isError = false;
                this.errorMsg = '';
            }
        },
	};
</script>
<style>
    @import url('../../css/base/input.css');    
</style>
