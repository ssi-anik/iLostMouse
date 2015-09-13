package com.example.anik.ilostmymouse.helpers;

import android.content.Context;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;

/**
 * Created by Anik on 12-Sep-15, 012.
 */
public class CustomGestureHandler implements View.OnTouchListener {

    private static final int SWIPE_THRESHOLD = 1;
    private static final int SWIPE_VELOCITY_THRESHOLD = 5;
    private float previous_x_coordinate = -1;
    private float previous_y_coordinate = -1;
    private GestureDetector gestureDetector;

    public CustomGestureHandler(Context context) {
        gestureDetector = new GestureDetector(context, new GestureListener());
    }

    public boolean onTouch(final View view, final MotionEvent motionEvent) {
        final int action = motionEvent.getAction();
        if (gestureDetector.onTouchEvent(motionEvent))
            return true;
        switch (action & MotionEvent.ACTION_MASK) {

            case MotionEvent.ACTION_DOWN: {
                previous_x_coordinate = motionEvent.getX();
                previous_y_coordinate = motionEvent.getY();
                return true;
            }

            case MotionEvent.ACTION_MOVE: {
                float current_x_coordinate = motionEvent.getX();
                float current_y_coordinate = motionEvent.getY();
                float diffX = current_x_coordinate - previous_x_coordinate;
                float diffY = current_y_coordinate - previous_y_coordinate;
                if (Math.abs(diffX) > Math.abs(diffY)) {
                    if (Math.abs(diffX) > SWIPE_THRESHOLD) {
                        if (diffX > 0) {
                            onSwipeRight((int) diffX, (int) diffY);
                        } else {
                            onSwipeLeft((int) diffX, (int) diffY);
                        }
                    }
                } else {
                    if (Math.abs(diffY) > SWIPE_THRESHOLD) {
                        if (diffY > 0) {
                            onSwipeDown((int) diffX, (int) diffY);
                        } else {
                            onSwipeUp((int) diffX, (int) diffY);
                        }
                    }
                }

                previous_x_coordinate = current_x_coordinate;
                previous_y_coordinate = current_y_coordinate;
                return true;
            }

            case MotionEvent.ACTION_UP:
                previous_x_coordinate = -1;
                previous_y_coordinate = -1;
                return true;
            default:
                return false;
        }
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

    private final class GestureListener extends GestureDetector.SimpleOnGestureListener {

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
    }
}
