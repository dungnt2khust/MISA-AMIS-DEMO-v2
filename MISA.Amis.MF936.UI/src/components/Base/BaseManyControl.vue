<template lang="">
	<div class="manycontrol non-select-text" :class="{'manycontrol--selected': enable}">
		<div @click="manyControlOnClick()" class="manycontrol__label">
			Thực hiện hàng loạt
		</div>
		<ul v-show="manyControlState" class="manycontrol__list">
			<li v-for="(item, index) in listManyControl" @click="choose(index)" class="manycontrol__item" :key="index">
				{{ item['name'] }}
			</li>
		</ul>
	</div>
</template>
<script>
	export default {
        name: 'BaseManyControl',
		props: {
			enable: {
				type: Boolean,
				default: false
			},
			listManyControl: {
				type: Array,
				default: function() {
					return []
				}
			}
		},
		data() {
			return {
				manyControlState: false
			}
		},
		methods: {
			/**
			 * Sự kiện ấn nút nếu enable thì hiện những chức năng hàng loạt
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			manyControlOnClick() {
				if (this.enable) {
					this.manyControlState = true;
				}
			},
			/**
			 * Chọn một chức năng
			 * @param {Number} index
			 * CreatedBy: NTDUNG (27/09/2021)
			 */
			choose(index) {
				this.listManyControl[index]['function']();
				this.manyControlState = false;
			}
		}

    };
</script>
<style>
	@import url('../../css/base/manycontrol.css');
</style>
