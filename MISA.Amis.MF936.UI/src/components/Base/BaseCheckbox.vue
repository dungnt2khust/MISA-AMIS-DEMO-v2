<template lang="">
	<div
		:tabindex="disable ? '' : 0"
		class="checkbox"
		@click="checkboxOnClick()"
		@keydown.enter="checkboxOnClick()"
		:class="{
			'checkbox--selected': checkboxState,
			'checkbox--disable': disable,
		}"
	>
		<div class="checkbox__border">
			<div class="checkbox__main">
				<div class="checkbox__icon"></div>
			</div>
		</div>
		<span v-if="label != ''" class="checkbox__label"> {{ label }}</span>
	</div>
</template>
<script>
	export default {
		name: "BaseCheckbox",
		props: {
			label: {
				type: String,
				default: "",
			},
			disable: {
				type: Boolean,
				default: false,
			},
			state: {
				type: [Number, Boolean],
				default: false,
			},
		},
		data() {
			return {
				checkboxState: this.state ? true : false,
			};
		},
		methods: {
			/**
			 * Bắt sự kiện click vào checkbox
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			checkboxOnClick() {
				if (!this.disable) this.checkboxState = !this.checkboxState;
			},
		},
		watch: {
			/**
			 * Khi state truyền vào thay đổi thì gán vào checkbox
			 * @param {Boolean} newState
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			state: function(newState) {
				this.checkboxState = newState ? true : false;
			},
			/**
			 * Khi checkboxState thay đổi thì emit ra ngoài component cha
			 * @param {Boolean} newState
			 * CreatedBy: NTDUNG (01/09/2021)
			 */
			checkboxState: function(newState) {
				if (this.state === true || this.state === false)
					this.$emit("input", newState);
				else 
					this.$emit("input", newState ? 1 : 0);
			},
		},
	};
</script>
<style lang=""></style>
