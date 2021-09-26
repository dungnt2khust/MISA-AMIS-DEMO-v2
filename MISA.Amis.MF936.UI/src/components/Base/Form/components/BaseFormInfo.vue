<template lang="">
	<div class="forminfo">
		<div class="forminfo__icon"></div>
		<p class="forminfo__content">{{ content }}</p>
	</div>
</template>
<script>
	export default {
		name: "BaseFormInfo",
		props: {
			content: {
				type: String,
				default: "",
			},
		},
	};
</script>
<style>
	@import url("../../../../css/base/form/components/forminfo.css");
</style>
