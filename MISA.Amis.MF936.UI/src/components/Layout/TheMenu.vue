<template lang="">
	<div id="menu" :class="{ 'menu--small': !menuState }">
		<div class="menu__home" :class="{ 'fx-center': !menuState }">
			<div v-show="menuState" class="menu__grid"></div>
			<div
				v-show="!menuState"
				@click="$emit('toggleMenu')"
				class="menu__toggle"
			>
				<div class="menu__toggle-icon"></div>
			</div>
			<div class="menu__logo" v-show="menuState"></div>
		</div>
		<div class="menu__list">
			<a
				v-for="(item, index) in menuList"
				:href="item['href']"
				class="menu__item"
				:class="{
					'fx-center': !menuState,
					'menu__item--selected': item['href'] == '/' + $route.path.split('/')[1] ,
				}"
				v-on="tooltipListeners(item['name'])"
				:key="index"
			>
				<div class="menu__icon-wrapper">
					<div
						class="menu__icon"
						:style="{
							'background-position':
								item['href'] == '/' + $route.path.split('/')[1] ? item.position2 : item.position,
							width: item.width,
							height: item.height,
						}"
					></div>
				</div>
				<span class="menu__label" v-show="menuState"> {{ item["name"] }} </span>
			</a>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../mixins/globalComponents/globalComponents.js";
	import menu from '../../mixins/menu.js'

	export default {
		name: "TheMenu",
		mixins: [globalComponents, menu],
		props: {
			menuState: {
				type: Boolean,
				default: false,
			},
		}
	};
</script>
<style>
	@import url('../../css/layout/menu.css');
</style>
