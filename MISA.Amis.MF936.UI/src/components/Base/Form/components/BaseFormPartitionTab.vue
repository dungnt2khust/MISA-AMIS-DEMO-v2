<template lang="">
	<div class="formpartitiontab ml-12 mr-12">
		<ul class="formpartitiontab__list">
			<li
				v-for="(item, index) in listPartitionTab"
                @click="choose(index)"
				class="formpartitiontab__item"
                :class="{'formpartitiontab__item--selected': index == currTab}"
				:key="index"
			>
				{{ item }}
			</li>
		</ul>
        <div class="formpartitiontab__body">
            <slot name="body"></slot>
        </div>
	</div>
</template>
<script>
	export default {
		name: "BaseFormPartitionTab",
		props: {
			listPartitionTab: {
				type: Array,
				default: function() {
					return [];
				},
			},
            currTab: {
                type: Number,
                default: -1
            }
		}, 
        methods: {
            /**
             * Chọn một tab
             * @param {Number} index
             * CreatedBy: NTDUNG (02/10/2021)
             */
            choose(index) {
                this.$emit('input', index);
            }
        }
	};
</script>
<style>
	@import url("../../../../css/base/form/components/formpartitiontab.css");
</style>
