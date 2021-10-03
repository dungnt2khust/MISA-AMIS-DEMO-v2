<template lang="">
	<div class="tab">
		<div
			v-for="(item, index) in listTab"
			@click="choose(index)"
			:class="{
				'tab__item--selected':
					$route.path.split('/')[2] == listTab[index]['path'].split('/')[2],
			}"
			class="tab__item"
			:key="index"
		>
			{{ item["name"] }}
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
				},
			},
		},
		methods: {
			choose(index) {
				this.$router.push(this.listTab[index]["path"]);
				this.$emit("input", index);
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/tab/tab.css");
</style>
