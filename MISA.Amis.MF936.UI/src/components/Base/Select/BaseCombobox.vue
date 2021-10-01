<template lang=""> 
    <div
		class="combobox"
		:class="{
			'combobox--up': comboboxType == 'up',
			'combobox--show': comboboxState,
			'combobox--focus': focusState,
			'combobox--error': comboboxError,
            'combobox--disable': !enable 
		}"
	>
		<span v-if="label != ''" class="label">
			{{ label }} <span v-if="required" class="text-red">*</span>
		</span>
		<div class="combobox__main">
			<div class="combobox__value">
				<input
					type="text"
					class="combobox__input"
                    :value="inputValue"
					:tabindex="tabIdx"
                    :readonly="!enable"
                    v-on="inputListeners"
				/>
			</div>
			<div
				@click="toggleCombobox()"
				class="combobox__button"
			>
				<div class="combobox__button-icon"></div>
			</div>
			<ul class="combobox__list">
				<li
					v-for="(item, index) in comboboxData"
					class="combobox__item"
					:class="{ 'combobox__item--selected': index == Number(currIdx) }"
                    @click="comboboxItemOnClick(index)"
					:key="index"
				>
					{{ item['name'] }}
				</li>
			</ul>
		</div>
	</div>
</template>
<script>
export default {
    name: 'BaseCombobox',
    props: {
        label: {
            type: String,
            default: ''
        },
        comboboxData: {
            type: Array,
            default: function() {
                return []
            }
        },
        currIdx: {
            type: [Number, String],
            default: -1
        },
        tabIdx: {
            type: Number,
            default: -1
        },
        required: {
            type: Boolean,
            default: false
        },
        comboboxType: {
            type: String,
            default: ''
        },
        enable: {
            type: Boolean,
            default: true
        }
    },
    data() {
        return {
            comboboxState: false,
            focusState: false,
            comboboxError: false
        }
    },
    computed: {
        /**
         * Bind giá trị của input
         * @return {String}
         * CreatedBy: NTDUNG (29/09/2021)
         */
        inputValue() {
            if (this.comboboxData[this.currIdx]) {
                return this.comboboxData[this.currIdx]['name'];
            } else {
                return '';
            }
        },
        /**
         * Bắt sự kiện của input
         * CreatedBy: NTDUNG (29/09/2021)
         */
        inputListeners() {
            return Object.assign({}, this.$listener, {
                focus: (event) => {
                    event.target.select();
                    this.focusState = true;
                },
                blur: () => {
                    this.focusState = false;
                }
            })
        }
    },
    methods: {
        /**
         * Sự kiện click vào combobox
         * @param {Number} index
         * CreatedBy: NTDUNG (29/09/2021)
         */
        comboboxItemOnClick(index) {
            this.$emit('input', index);
            this.comboboxState = false;
        },
        /**
         * Bật tắt combobox
         * CreatedBy: NTDUNG (29/09/2021)
         */
        toggleCombobox() {
            if (this.enable)
                this.comboboxState = !this.comboboxState;
        }
    }
}
</script>
<style>
    @import url('../../../css/base/combobox.css');
</style>