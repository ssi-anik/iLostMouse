package com.example.anik.ilostmymouse.helpers;

import android.content.Context;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;

/**
 * Created by Anik on 12-Sep-15, 012.
 */
public class CustomGestureHandler implements View.OnTouchListener {

    private GestureDetector gestureDetector;

    public CustomGestureHandler(Context context) {
        gestureDetector = new GestureDetector(context, new GestureListener());
    }

    public boolean onTouch(final View view, final MotionEvent motionEvent) {
        return gestureDetector.onTouchEvent(motionEvent);
    }

    public void onSwipeRight(int diffX, int diffY) {
    }

    public void onSwipeLeft(int diffX, int diffY) {
    }

    public void onSwipeUp(int diffX, int diffY) {
    }

    public void onSwipeDown(int diffX, int diffY) {
    }

    public void onSingleClick() {

    }

    public void onDoubleClick() {

    }

    public void onLongClick() {

    }

    private final class GestureListener extends GestureDetector.SimpleOnGestureListener {

        private static final int SWIPE_THRESHOLD = 1;
        private static final int SWIPE_VELOCITY_THRESHOLD = 5;

        @Override
        public boolean onDown(MotionEvent e) {
            return true;
        }

        @Override
        public boolean onSingleTapConfirmed(MotionEvent e) {
            onSingleClick();
            return super.onSingleTapUp(e);
        }

        @Override
        public boolean onDoubleTap(MotionEvent e) {
            onDoubleClick();
            return super.onDoubleTap(e);
        }

        @Override
        public void onLongPress(MotionEvent e) {
            onLongClick();
            super.onLongPress(e);
        }

        @Override
        public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
            boolean result = false;
            try {
                float diffY = e2.getY() - e1.getY();
                float diffX = e2.getX() - e1.getX();
                if (Math.abs(diffX) > Math.abs(diffY)) {
                    if (Math.abs(diffX) > SWIPE_THRESHOLD && Math.abs(velocityX) > SWIPE_VELOCITY_THRESHOLD) {
                        if (diffX > 0) {
                            onSwipeRight((int) diffX, (int) diffY);
                        } else {
                            onSwipeLeft((int) diffX, (int) diffY);
                        }
                    }
                } else {
                    if (Math.abs(diffY) > SWIPE_THRESHOLD && Math.abs(velocityY) > SWIPE_VELOCITY_THRESHOLD) {
                        if (diffY > 0) {
                            onSwipeDown((int) diffX, (int) diffY);
                        } else {
                            onSwipeUp((int) diffX, (int) diffY);
                        }
                    }
                }
            } catch (Exception exception) {
                exception.printStackTrace();
            }
            return result;
        }
    }
}
