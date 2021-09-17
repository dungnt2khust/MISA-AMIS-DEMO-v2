<template lang="">
	<div class="tab">
		<div
			v-for="(item, index) in listTab"
            @click="$emit('input', index)"
			:class="{ 'tab__item--selected': $route.path == listTab[index]['path']}"
			class="tab__item"
			:key="index"
		>
			{{ item['name'] }}                                       
		</div> 
	</div>
</template>
<script>
	export default {
		name: "BaseTab",
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
	@import url("../../css/base/tab.css");
</style>
