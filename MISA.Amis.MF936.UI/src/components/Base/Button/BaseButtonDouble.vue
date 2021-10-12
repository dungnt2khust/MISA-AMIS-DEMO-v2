<template lang="">
	<div class="button-double" :class="'button-double--' + type">
		<div
			@click="executeMethod(currFunction)"
			class="button button--green button--half-left"
		>
			{{ listMethods[currFunction]["Label"] }}
		</div>
		<div @click="showList = !showList" class="button button--green button--half-right"></div>
		<ul v-if="!hideList && showList" class="button-double__list">
			<li
				v-for="(item, index) in listMethods"
				class="button-double__item"
                v-on="tooltipListeners(item['Tooltip'])"
                @click="executeMethod(index)"
				:key="index"
			>
				{{ item["Label"] }}
			</li>
		</ul>
	</div>
</template>
<script>
    // LIBRARY
    import globalComponents from "../../../mixins/globalComponents/globalComponents.js"

	export default {
		name: "BaseButtonDouble",
        mixins: [globalComponents],
		props: {
			listMethods: {
				type: Array,
				default: function() {
					return [];
				},
			},
			type: {
				type: String,
				default: "",
			},
            hideList: {
                type: Boolean,
                default: false
            }
		},
		data() {
			return {
				currFunction: 0,
                showList: false
			};
		},
		methods: {
			/**
			 * Thực hiện chức năng
			 * @param {Number} index
			 * CreatedBy: NTDUNG (30/09/2021)
			 */
			executeMethod(index) {
				this.listMethods[index]["Method"]();
                this.showList = false;
                this.currFunction = index;
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/button/buttondouble.css");
</style>
