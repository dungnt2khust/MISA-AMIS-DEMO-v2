<template lang=""> 
    <div
		class="combobox"
		:class="{
			'combobox--up': comboboxType == 'up',
			'combobox--show': comboboxState,
			'combobox--focus': focusState,
			'combobox--error': comboboxError,
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
				/>
			</div>
			<div
				tabindex="0"
				@click="comboboxState = !comboboxState"
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
        inputValue() {
            if (this.comboboxData[this.currIdx]) {
                return this.comboboxData[this.currIdx]['name'];
            } else {
                return '';
            }
        }
    },
    methods: {
        comboboxItemOnClick(index) {
            this.$emit('input', index);
            this.comboboxState = false;
        }
    }
}
</script>
<style>
    @import url('../../../css/base/combobox.css');
</style>