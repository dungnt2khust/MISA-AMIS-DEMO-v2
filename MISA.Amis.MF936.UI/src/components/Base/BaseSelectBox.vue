<template lang="">
	<div
		class="selectbox"
		:class="{
			'selectbox--up': selectboxType == 'up',
			'selectbox--show': selectboxState,
		}"
	>
		<div
			v-on="tooltipListeners(selectboxData[currIdx])"
			@click="selectboxState = !selectboxState"
			class="selectbox__value"
		>
			{{ selectboxData[currIdx] }}
		</div>
		<div
			class="selectbox__icon"
			@click="selectboxState = !selectboxState"
		></div>
		<ul class="selectbox__list">
			<li
				v-for="(item, index) in selectboxData"
				class="selectbox__item"
				:class="{ 'selectbox__item--selected': index == currIdx }"
				@click="selectboxItemOnClick(index)"
				:key="index"
			>
				{{ item }}
			</li>
		</ul>
	</div>
</template>
<script>
	// LIBRARY
	import Mixin from "../../Mixins/listeners.js";

	export default {
		name: "BaseSelectBox",
		mixins: [Mixin],
		props: {
			selectboxType: {
				type: String,
				default: "down",
			},
		},
		data() {
			return {
				currIdx: 0,
				selectboxData: ["CÔNG TY CỔ PHẦN MISA", "CÔNG TY KHÁC"],
				selectboxState: false,
			};
		},
		methods: {
			/**
			 * Xử lý sự kiện click vào selectbox item
			 * CreatedBy: NTDUNG (28/08/2021)
			 * @param {number} index chỉ số của item
			 */
			selectboxItemOnClick(index) {
				this.currIdx = index;
				this.selectboxState = false;
			},
		},
	};
</script>
<style lang=""></style>
