<template lang="">
	<div class="tab2">
		<div
			v-for="(item, index) in listTab"
            @click="$emit('input', index)"
			:class="{ 'tab2__item--selected': index == 1}"
			class="tab2__item"
			:key="index"
		>
			{{ item['name'] }}                                       
		</div> 
	</div>
</template>
<script>
	export default {
		name: "BaseTab2",
		props: {	
			listTab: {
				type: Array,
				default: function() {
					return [];
				}
			}
		}	
	};
</script>
<style>
	@import url("../../../css/base/tab/tab2.css");
</style>
