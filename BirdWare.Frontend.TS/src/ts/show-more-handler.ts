import { ref, computed, reactive } from "vue";

export function useShowMoreHandler(itemCount: number) {
    const isShowMore = ref(false);
    const valueOfTen = 10;

    const state = reactive({
        itemCount: 0 as number
    });

    state.itemCount = itemCount;

    const tooManyItems = computed(() => {
        return state.itemCount > valueOfTen && !isShowMore.value;
    });

    const isShowingAllItems = computed(() => {
        return state.itemCount > valueOfTen && isShowMore.value;
    });

    const takeCount = computed(() => {
        if (tooManyItems.value) {
            isShowMore.value = false;
            return valueOfTen;
        }
        return state.itemCount;
    });

    function toggleShowMore() {
        isShowMore.value = !isShowMore.value;
    }

    function updateItemCount(newCount: number) {
        state.itemCount = newCount;
    }
    
    return {
        tooManyItems,
        isShowingAllItems,
        takeCount,
        toggleShowMore,
        updateItemCount
    };
}