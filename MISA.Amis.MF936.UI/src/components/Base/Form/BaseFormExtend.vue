<template lang="">
    <div class="formextend__overlay">
		<div
			class="formextend"
			:class="{ 'formextend--fullscreen': fullscreen }"
		>
			<div class="formextend__header fx-space-between fx-start-ver">	
				<slot name="header"></slot>
				<div class="formextend__close">
					<base-help />
					<base-cancel :method="hideForm"/>
				</div>
			</div>
			<div class="formextend__body" @scroll="scrollForm()">
				<slot name="body"></slot>
            </div>
			<div class="formextend__footer">
				<slot name="footer"></slot>
			</div>
			<div
				@click="fullscreen = !fullscreen"
				class="formextend__extend"
                :style="{'transform': fullscreen ? 'rotate(180deg)' : 'rotate(0)'}"
			></div>
		</div>
	</div> 
</template>
<script>
// COMPONENTS
import BaseHelp from './components/BaseHelp.vue'
import BaseCancel from './components/BaseCancel.vue'

export default {
    name: 'BaseFormExtend',
	components: {
		BaseHelp,
		BaseCancel
	},
    data() {
        return { 
            fullscreen: false
        }
    },
	methods: {
		hideForm() {
			this.$emit('input', false);
		},
		scrollForm() {
			this.$bus.$emit('hideListGrid');
		}
	}
}
</script>
<style>
    @import url('../../../css/base/form/formextend.css');
</style>