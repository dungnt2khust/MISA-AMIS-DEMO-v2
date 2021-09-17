<template lang="">
	<div class="manycontrol non-select-text" :class="{'manycontrol--selected': manyControlState}">
		<div @click="manyControlOnClick()" class="manycontrol__label">
			Thực hiện hàng loạt
		</div>
		<ul v-show="manyControlState" class="manycontrol__list">
			<li @click.stop="deleteMany()" class="manycontrol__item">
				Xoá
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
				defaule: false
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
			}
		}

    };
</script>
<style>
	@import url('../../css/base/manycontrol.css');
</style>
