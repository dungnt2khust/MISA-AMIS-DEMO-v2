<template lang="">
	<div
		@click="dropdownButtonOnClick()"
		class="dropdownbutton"
		:class="{ 'dropdownbutton--selected': showList, 'dropdownbutton--disable': !enable }"
	>
		<div class="dropdownbutton__icon"></div>
	</div>
</template>
<script>
	export default {
		name: "BaseDropdownButton",
		props: {
			enable: {
				type: Boolean,
				default: true
			},
			showList: {
				type: Boolean,
				default: false
			}
		},
		methods: {
			dropdownButtonOnClick() {
				if (this.enable) {	
					this.$emit('toggleListGrid');
				}	
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/dropdownbutton.css");
</style>
