package com.itsigned.huqariq.activity

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.EditText
import androidx.fragment.app.Fragment
import com.itsigned.huqariq.R
import com.itsigned.huqariq.fragment.UnityFragment

class MainUnityActivity : AppCompatActivity() {
    var unityFragment: UnityFragment? = null
    var mTTSInput : EditText? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_unity)
        mTTSInput = findViewById<View>(R.id.ttsInput) as EditText

        unityFragment = UnityFragment()
        transitionFragment(unityFragment)
    }


    /**
     * Metodo para transicionar el fragmento dado
     * @param fragment el fragmento a transicionar
     * @return un boleano indicando el exito de la transacción
     */
    private fun transitionFragment(fragment: Fragment?): Boolean {
        if (fragment == null) return false
        val transaction = supportFragmentManager.beginTransaction()
        transaction.replace(R.id.fragmentTransition, fragment, "Fragment")
        transaction.addToBackStack(fragment.tag)
        transaction.commit()
        return true
    }

    fun SendTTSText(view: View) {
        unityFragment?.SendTTSText(mTTSInput?.text.toString())
    }
}